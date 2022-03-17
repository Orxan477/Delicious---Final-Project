#pragma checksum "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3a7d5f780ade730f33a9216e01d9de9eef6dbb01"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Menu_MyOrder), @"mvc.1.0.view", @"/Views/Menu/MyOrder.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.Core.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Reservation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Menu;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Account;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a7d5f780ade730f33a9216e01d9de9eef6dbb01", @"/Views/Menu/MyOrder.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1a265a8357939eaaedc63aa3d0a6938115de1f8a", @"/Views/_ViewImports.cshtml")]
    public class Views_Menu_MyOrder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HomeVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/otherPage.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/js/menu.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
  
    ViewData["Title"] = "MyOrder";
    int n = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <div class=""row"">
        <table class=""table"" style=""margin-top:150px"">
            <thead>
                <tr>
                    <th scope=""col"">No</th>
                     <th scope=""col"">Address</th>
                        <th scope=""col"">Phone</th>
                        <th scope=""col"">Count</th>
                        <th scope=""col"">Order Date</th>
                        <th scope=""col"">Order Total</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 21 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                 foreach (var item in Model.FullOrders)
               {

#line default
#line hidden
#nullable disable
            WriteLiteral("                   <tr>\r\n                    <th scope=\"row\">");
#nullable restore
#line 24 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                               Write(n);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 25 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                   Write(item.BillingAdress.Adress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 26 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                   Write(item.BillingAdress.AppUser.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 27 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                   Write(item.Orders.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 28 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                   Write(item.CreatedAt);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 29 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                   Write(item.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral(" $</td>\r\n                </tr>\r\n");
#nullable restore
#line 31 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Views\Menu\MyOrder.cshtml"
                n++;
               }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Script", async() => {
                WriteLiteral("\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3a7d5f780ade730f33a9216e01d9de9eef6dbb018171", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3a7d5f780ade730f33a9216e01d9de9eef6dbb019266", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
            }
            );
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HomeVM> Html { get; private set; }
    }
}
#pragma warning restore 1591