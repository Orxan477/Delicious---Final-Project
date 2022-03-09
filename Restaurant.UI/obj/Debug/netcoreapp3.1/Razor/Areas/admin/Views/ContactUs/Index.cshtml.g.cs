#pragma checksum "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e2c4fbb7424ec24732e4cc22027f63d026909050"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_admin_Views_ContactUs_Index), @"mvc.1.0.view", @"/Areas/admin/Views/ContactUs/Index.cshtml")]
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
#line 1 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Core.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Footer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Reservation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Menu;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Team;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Position;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Home.HomeIntro;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\_ViewImports.cshtml"
using Restaurant.Business.ViewModels.Home.About;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2c4fbb7424ec24732e4cc22027f63d026909050", @"/Areas/admin/Views/ContactUs/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3c5919433bbbc2adee4c7fbc35e1cb98264cccd", @"/Areas/admin/Views/_ViewImports.cshtml")]
    public class Areas_admin_Views_ContactUs_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ContactUs>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Check", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
  
    ViewData["Title"] = "Index";
    int n = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12 grid-margin stretch-card"">
                <div class=""card"">
                  <div class=""card-body"">
                    <h4 class=""card-title"">Contact Us</h4>
                    <div class=""table-responsive"">
                      <table class=""table"">
                        <thead>
                          <tr>
                            <th>No</th>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Subject</th>
                            <th>Message</th>
                            <th>Sent Date</th>
                            <th>Setting</th>
                          </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 26 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                          foreach (var contact in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                         <tr>\r\n                            <td>");
#nullable restore
#line 29 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                           Write(n);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 30 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                           Write(contact.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 31 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                           Write(contact.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 32 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                           Write(contact.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 33 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                           Write(contact.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 34 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                           Write(contact.SentDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e2c4fbb7424ec24732e4cc22027f63d0269090509469", async() => {
                WriteLiteral("\r\n                                        <button type=\"submit\" class=\"btn btn-success\"><i class=\"mdi mdi-check-circle-outline\"></i></button>\r\n                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 36 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                                                                                WriteLiteral(contact.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n                                </tr>\r\n");
#nullable restore
#line 41 "D:\HP\Documents\Private\Code Academy\FinallProject\Restaurant.UI\Areas\admin\Views\ContactUs\Index.cshtml"
                                n++;
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                      </table>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ContactUs>> Html { get; private set; }
    }
}
#pragma warning restore 1591
