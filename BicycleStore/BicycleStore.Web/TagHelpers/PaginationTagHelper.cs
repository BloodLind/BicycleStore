
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BicycleStore.Web.TagHelpers
{
    public class PaginationTagHelper:TagHelper
    {

     private   IUrlHelperFactory urlHelperFactory;
        public PaginationTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public int CountPages { get; set; }
        public int CurrentPage { set; get; }

        public string PageAction { set; get; }

        public Dictionary<string, object> UrlRouteValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

         
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");


            for(int i =0;i<CountPages;i++)
                tag.InnerHtml.AppendHtml(CreateTag(i+1, urlHelper));
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");

            if (pageNumber == CurrentPage)
                item.AddCssClass("active");
            else
            {
                UrlRouteValues["page"] = pageNumber;

                  link.Attributes["href"] = urlHelper.Action(PageAction, UrlRouteValues);
            
                
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
