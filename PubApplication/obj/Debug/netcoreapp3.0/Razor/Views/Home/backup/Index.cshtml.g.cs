#pragma checksum "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5aed05496f81031cc35f05d327bff0a75ae959ce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_backup_Index), @"mvc.1.0.view", @"/Views/Home/backup/Index.cshtml")]
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
#line 1 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\_ViewImports.cshtml"
using PubApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\_ViewImports.cshtml"
using PubApplication.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\_ViewImports.cshtml"
using PubApplication.Models.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\_ViewImports.cshtml"
using PubApplication.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\_ViewImports.cshtml"
using System.Text.Json;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5aed05496f81031cc35f05d327bff0a75ae959ce", @"/Views/Home/backup/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5413b9fdd0072fc5cb0a1014898abe92ebe4cec9", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_backup_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Items", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Buy", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-white typeText"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    PubItems DrinkItem = ViewBag.DrinkItem;
    PubItems SnackItem = ViewBag.SnackItem;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    .deal {
        min-height: 300px;
        max-height: 300px;
        position: relative;
    }

    .typeText {
        position: absolute;
        top: 40%;
        left: 0%;
        width: 100%;
        font-size: xx-large;
        text-align: center;
    }

    .typeImg {
        display: block;
        margin-left: auto;
        margin-right: auto;
        height: inherit;
        max-height:inherit;
        max-width:100%;
    }
    .warning {
        height: 40%;
        font-size: large;
        position: absolute;
        display: block;
    }
</style>
<div class=""container mt-md-3"">
    <div class=""row"">
        <div class=""col-12"">
            <h1 class=""display-4 text-center"">Order Menu</h1>
        </div>
    </div>
    <div class=""row mt-md-5"">
        <div class=""col-lg-5 mt-5 mt-lg-0"">
            <div class=""deal border border-dark border-bottom-0 rounded-top"">
                <div class=""container"">
                    <div class=""row"" sty");
            WriteLiteral("le=\"max-height:inherit\">\r\n");
#nullable restore
#line 48 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                         if (DrinkItem != null)
                        {
                            var DrinkImage = (DrinkItem.ItemImagePath ?? "RedLion.png");

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"col-md-12 mt-3\" style=\"height:230px\">\r\n                                <image asp-append-version=\"true\"");
            BeginWriteAttribute("src", " src=\"", 1494, "\"", 1517, 2);
            WriteAttributeValue("", 1500, "/imgs/", 1500, 6, true);
#nullable restore
#line 52 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
WriteAttributeValue("", 1506, DrinkImage, 1506, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-responsive typeImg""> </image>
                            </div>
                            <div class=""col-12 container"" style=""max-height:50px"">
                                <div class=""row"">
                                    <div class=""col-6"">
                                        <h6 class=""text-dark text-center font-weight-bold""> ");
#nullable restore
#line 57 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                                                                                       Write(DrinkItem.ItemName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                                    </div>\r\n                                    <div class=\"col-3\">\r\n                                        <h6 class=\"text-dark text-center font-weight-bold\"> Price: ");
#nullable restore
#line 60 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                                                                                              Write(DrinkItem.ItemPrice.ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                                    </div>\r\n                                    <div class=\"col-3 text-dark text-center\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5aed05496f81031cc35f05d327bff0a75ae959ce10233", async() => {
                WriteLiteral("\r\n                                            <button type=\"submit\" class=\"btn btn-secondary\"> Add </button>\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 63 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                                                                                                      WriteLiteral(DrinkItem.ItemId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 69 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <div class=""col-md-12"">
                                <div class=""col-12 text-warning warning"">
                                    There are currently no drinks on sale!
                                </div>
                            </div>
");
#nullable restore
#line 77 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"deal bg-dark rounded-bottom\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5aed05496f81031cc35f05d327bff0a75ae959ce14397", async() => {
                WriteLiteral("Drinks Menu");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
        <div class=""col-lg-5 offset-lg-2 mt-5 mt-lg-0"">
            <div class=""deal border border-dark border-bottom-0 rounded-top"">
                <div class=""container"">
                    <div class=""row"" style=""max-height:inherit"">
");
#nullable restore
#line 89 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                         if (SnackItem != null)
                        {
                            var SnackImage = (SnackItem.ItemImagePath ?? "RedLion.png");

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"col-md-12 mt-3\" style=\"max-height:230px\">\r\n                                <image asp-append-version=\"true\"");
            BeginWriteAttribute("src", " src=\"", 3913, "\"", 3936, 2);
            WriteAttributeValue("", 3919, "/imgs/", 3919, 6, true);
#nullable restore
#line 93 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
WriteAttributeValue("", 3925, SnackImage, 3925, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-responsive typeImg""> </image>
                            </div>
                            <div class=""col-12 container"" style=""max-height:50px"">
                                <div class=""row"">
                                    <div class=""col-6"">
                                        <h6 class=""text-dark text-center font-weight-bold""> ");
#nullable restore
#line 98 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                                                                                       Write(SnackItem.ItemName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                                    </div>\r\n                                    <div class=\"col-3\">\r\n                                        <h6 class=\"text-dark text-center font-weight-bold\"> Price: ");
#nullable restore
#line 101 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                                                                                              Write(SnackItem.ItemPrice.ToString("C"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                                    </div>\r\n                                    <div class=\"col-3 text-dark text-center\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5aed05496f81031cc35f05d327bff0a75ae959ce18763", async() => {
                WriteLiteral("\r\n                                            <button type=\"submit\" class=\"btn btn-secondary\"> Add </button>\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 104 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                                                                                                      WriteLiteral(SnackItem.ItemId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 110 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <div class=""col-md-12"">
                                <div class=""col-12 text-warning warning"">
                                    There are currently no snacks on sale!
                                </div>
                            </div>
");
#nullable restore
#line 118 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Home\backup\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"deal bg-dark rounded-bottom\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5aed05496f81031cc35f05d327bff0a75ae959ce22930", async() => {
                WriteLiteral("Snacks Menu");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
