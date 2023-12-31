﻿using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Interfaces;
using Infastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Persistence.Data.Seeding
{
    internal class SeedingInitializer : IDisposable
    {
        private bool disposedValue;
        public IUnitOfWork<TbCustomerField> uowCustomerFields { get; }
        private ApplicationDbContext context { get; }
        public SeedingInitializer(IUnitOfWork<TbCustomerField> uowCustomerFields, ApplicationDbContext applicationDbContext)
        {
            this.uowCustomerFields = uowCustomerFields;
            context = applicationDbContext;
        }

        public async Task<int> Seed()
        {
            int setup = -1;

            if (context.TbDiscountTypes.Count() == 0)
            {
                await context.TbDiscountTypes.AddAsync(new TbDiscountType() { DiscountType = DiscountTypeEn.Percentage.ToString() });
                await context.TbDiscountTypes.AddAsync(new TbDiscountType() { DiscountType = DiscountTypeEn.Coupon.ToString() });
                await context.SaveChangesAsync();
            }

            if (context.TbViewTypes.Count() == 0)
            {
                await context.TbViewTypes.AddAsync(new TbViewType() { TypeName = "SimpleField" });
                await context.TbViewTypes.AddAsync(new TbViewType() { TypeName = "DropdownLists" });
                await context.SaveChangesAsync();
            }



            if (context.TbCurrencies.Count() == 0)
            {
                await context.TbCurrencies.AddAsync(new TbCurrency() { Name = "USD", Symbol = "$" });
                await context.TbCurrencies.AddAsync(new TbCurrency() { Name = "EUR", Symbol = "€" });
                await context.SaveChangesAsync();
            }

            if (context.TbDiscounts.Count() == 0)
            {
                var currency = await context.TbCurrencies?.FirstOrDefaultAsync(x => x.Name == CurrencyEn.EUR.ToString()) ?? default;
                var percentageType = await context.TbDiscountTypes.FirstOrDefaultAsync(x => x.DiscountType == DiscountTypeEn.Percentage.ToString()) ?? default;
                var couponType = await context.TbDiscountTypes.FirstOrDefaultAsync(x => x.DiscountType == DiscountTypeEn.Coupon.ToString()) ?? default;

                if (currency == default || percentageType == default || couponType == default) return setup;

                await context.TbDiscounts.AddAsync(new TbDiscount()
                {
                    DiscountName = "PriceLists",
                    CurrencyId = currency.Id,
                    DiscountTypeId = percentageType?.Id ?? 1,
                    PriorityOrderId = 0,
                    Price = 5,
                });

                await context.TbDiscounts.AddAsync(new TbDiscount()
                {
                    DiscountName = "Promotions",
                    CurrencyId = currency.Id,
                    DiscountTypeId = percentageType.Id,
                    PriorityOrderId = 1,
                    Price = 10,
                });

                await context.TbDiscounts.AddAsync(new TbDiscount()
                {
                    DiscountName = "Coupons",
                    CurrencyId = currency.Id,
                    DiscountTypeId = couponType.Id,
                    PriorityOrderId = 2,
                    Price = 10,
                });
            }

            if (context.TbCustomers.Count() == 0)
            {
                var viewId = 0;
                TbView? view = null;
                var customer = new TbCustomer()
                {
                    Name = "MockCustName",
                    LastName = "MockCustLastName",
                    Address = "MockCustAddress",
                };

                await context.TbCustomers.AddAsync(customer);
                var customerId = await context.SaveChangesAsync();

                if (context.TbViews.Count() == 0)
                {
                    view = new TbView() { TypeId = 1, Name = "Email" };
                    await context.TbViews.AddAsync(view);
                    viewId = await context.SaveChangesAsync();
                }

                await customer.AddDynamicViews(uowCustomerFields, viewId, null);
            }

            if (context.TbOrders.Count() == 0)
            {
                var customer = await context.TbCustomers.FirstOrDefaultAsync(x => x.Name == "MockCustName");
                if (customer != null)
                {
                    await context.TbOrders.AddAsync(new TbOrder()
                    {
                        CustomerId = customer.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        ProductId = 1,
                    });

                    await context.SaveChangesAsync();

                    var order = await context.TbOrders.FirstOrDefaultAsync(x => x.CustomerId == customer.Id);

                    var allDiscounts = await context.TbDiscounts.ToListAsync();
                    order.TbOrderDiscounts.Add(new TbOrderDiscount()
                    {
                        OrderId = order.Id,
                        DiscountId = allDiscounts.FirstOrDefault(x => x.DiscountName == "PriceLists").Id,
                    });

                    order.TbOrderDiscounts.Add(new TbOrderDiscount()
                    {
                        OrderId = order.Id,
                        DiscountId = allDiscounts.FirstOrDefault(x => x.DiscountName == "Promotions").Id,
                    });

                    order.TbOrderDiscounts.Add(new TbOrderDiscount()
                    {
                        OrderId = order.Id,
                        DiscountId = allDiscounts.FirstOrDefault(x => x.DiscountName == "Coupons").Id,
                    });

                    order.TbSubscriptions.Add(new TbSubscription()
                    {
                        SubscriptionType = "Recurring",
                        OrderId = order.Id,
                        Price = 340,
                        DatePlan = (int)DatePlanEn.Monthly,
                        DateNum = 6
                    });
                }


            }

            if (context.ChangeTracker.HasChanges())
            {
                setup = 1;
                await context.SaveChangesAsync();
            }
            return setup;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ((IDisposable)context).Dispose();
                }
                disposedValue = true;
            }
        }

        ~SeedingInitializer()
        {
            Dispose(disposing: false);
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}