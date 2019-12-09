#pragma checksum "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Shared\ToastMessage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ecefbfa10637a908663a5bdeeb4fa353032d5e8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_ToastMessage), @"mvc.1.0.view", @"/Views/Shared/ToastMessage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ecefbfa10637a908663a5bdeeb4fa353032d5e8e", @"/Views/Shared/ToastMessage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5413b9fdd0072fc5cb0a1014898abe92ebe4cec9", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_ToastMessage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ToastAlertViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("toast-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Card image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Shared\ToastMessage.cshtml"
  
        var photoPath = "~/imgs/" + (Model.ToastImagePath ?? "RedLionText.png");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .toast-img {
        max-height: 80px;
        width: auto;
        margin: auto;
        display: block;
    }

    .fade {
      transition: opacity 1.5s linear !important;
    }
</style>

<!-- Then put toasts within -->
<div class=""toast"" role=""alert"" aria-live=""assertive"" aria-atomic=""true"" data-delay=""250"">
    <div class=""toast-header"">
        <strong id=""ToastyMessage"" class=""mr-auto"">");
#nullable restore
#line 23 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Shared\ToastMessage.cshtml"
                                              Write(Model.ToastTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</strong>
        <button type=""button"" class=""ml-2 mb-1 close"" data-dismiss=""toast"" aria-label=""Close"">
            <span aria-hidden=""true"">&times;</span>
        </button>
    </div>
    <div class=""toast-body"">
        <div class=""contianer"">
            <div class=""row"">
                <div class=""col-4"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ecefbfa10637a908663a5bdeeb4fa353032d5e8e6074", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Shared\ToastMessage.cshtml"
                                    WriteLiteral(photoPath);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("src", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 32 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Shared\ToastMessage.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n                <div class=\"col-8\">\r\n                    <p> ");
#nullable restore
#line 35 "C:\Users\Ben\Source\Repos\ISAD251-Coursework-Project\PubApplication\Views\Shared\ToastMessage.cshtml"
                   Write(Model.ToastBody);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ToastAlertViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591