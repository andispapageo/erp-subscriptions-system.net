﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infastructure.Data.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
               name: "TbCustomer",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   Name = table.Column<string>(maxLength: 128, nullable: false),
                   LastName = table.Column<string>(maxLength: 128, nullable: false),
                   Address = table.Column<string>(maxLength: 128, nullable: false),
                   //(..)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_TbCustomer", x => new { x.Id });
               });

            migrationBuilder.CreateTable(
              name: "TbDiscountTypes",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  DiscountType = table.Column<string>(maxLength: 128, nullable: false),

              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbDiscountTypes", x => new { x.Id });
              });

            migrationBuilder.CreateTable(
                 name: "TbCurrency",
                 columns: table => new
                 {
                     Id = table.Column<int>(nullable: false)
                     .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                     Name = table.Column<string>(maxLength: 128, nullable: false),
                     Symbol = table.Column<string>(maxLength: 10, nullable: false),
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_TbCurrency", x => new { x.Id });
                 });


            migrationBuilder.CreateTable(
                name: "TbDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscountTypeId = table.Column<int>(nullable: false),
                    DiscountName = table.Column<string>(maxLength: 128, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    PriorityOrderId = table.Column<int>(nullable: false)
                    //(..)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDiscount", x => new { x.Id });
                    table.ForeignKey(
                        name: "FK_TbDiscount_TbDiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "TbDiscountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                       name: "FK_TbDiscounts_TbCurrency_CurrencyId",
                       column: x => x.CurrencyId,
                       principalTable: "TbCurrency",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                     .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                    //(..)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrder", x => new { x.Id });

                    table.ForeignKey(
                      name: "FK_TbOrder_TbCustomer_CustomerId",
                      column: x => x.CustomerId,
                      principalTable: "TbCustomer",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbOrderDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    DiscountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbOrderDiscount", x => new { x.Id });
                    table.ForeignKey(
                        name: "FK_TbOrderDiscount_TbOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "TbOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_TbOrderDiscount_TbDiscount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "TbDiscount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
              name: "TbSubscriptions",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  OrderId = table.Column<int>(nullable: false),
                  SubscriptionType = table.Column<string>(maxLength: 128, nullable: false),
                  Price = table.Column<decimal>(nullable: false),
                  DatePlan = table.Column<int>(nullable: false),
                  DateNum = table.Column<int>(nullable: false),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbSubscriptions", x => new { x.Id });
                  table.ForeignKey(
                      name: "FK_TbSubscriptions_TbOrder_OrderId",
                      column: x => x.OrderId,
                      principalTable: "TbOrder",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

            migrationBuilder.CreateTable(
              name: "TbViewType",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  TypeName = table.Column<string>(maxLength: 128, nullable: false),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbViewType", x => new { x.Id });

              });

            migrationBuilder.CreateTable(
              name: "TbView",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  TypeId = table.Column<int>(nullable: false),
                  Name = table.Column<string>(maxLength: 128, nullable: false),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbView", x => new { x.Id });
                  table.ForeignKey(
                   name: "FK_TbView_TbType_TypeId",
                   column: x => x.TypeId,
                   principalTable: "TbViewType",
                   principalColumn: "Id",
                   onDelete: ReferentialAction.Cascade);

              });

            migrationBuilder.CreateTable(
              name: "TbField",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  ViewId = table.Column<int>(nullable: false),
                  Name = table.Column<string>(maxLength: 128, nullable: false),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbField", x => new { x.Id });
                  table.ForeignKey(
                    name: "FK_TbField_TbView_ViewId",
                    column: x => x.ViewId,
                    principalTable: "TbView",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
              });

            migrationBuilder.CreateTable(
              name: "TbCustomerFields",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  CustomerId = table.Column<int>(nullable: false),
                  ViewId = table.Column<int>(nullable: false),
                  ViewValue = table.Column<string>(maxLength: 208, nullable: true),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbCustomerFields", x => new { x.Id });
                  table.ForeignKey(
                    name: "FK_TbCustomerFields_TbView_ViewId",
                    column: x => x.ViewId,
                    principalTable: "TbView",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);

                  table.ForeignKey(
                    name: "FK_TbCustomerFields_TbCustomer_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "TbCustomer",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
              });

            migrationBuilder.CreateTable(
              name: "TbCustomerFieldsHistory",
              columns: table => new
              {
                  Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                  CustomerId = table.Column<int>(nullable: false),
                  ViewId = table.Column<int>(nullable: false),
                  OldViewValue = table.Column<string>(maxLength: 208, nullable: true),
                  NewViewValue = table.Column<string>(maxLength: 208, nullable: true),
                  UpdateDate = table.Column<DateTime>(nullable: false),
                  //(..)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TbCustomerFieldsHistory", x => new { x.Id });
                  table.ForeignKey(
                    name: "FK_TbCustomerFieldsHistory_TbView_ViewId",
                    column: x => x.ViewId,
                    principalTable: "TbView",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);

                  table.ForeignKey(
                    name: "FK_TbCustomerFieldsHistory_TbCustomer_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "TbCustomer",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
              });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TbCustomer");

            migrationBuilder.DropTable(
                name: "TbDiscountTypes");

            migrationBuilder.DropTable(
                name: "TbDiscount");

            migrationBuilder.DropTable(
                name: "TbCurrency");

            migrationBuilder.DropTable(
               name: "TbOrder");

            migrationBuilder.DropTable(
                name: "TbOrderDiscount");

        }
    }
}
