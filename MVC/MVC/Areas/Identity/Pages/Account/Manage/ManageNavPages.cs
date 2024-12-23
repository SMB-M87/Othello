﻿#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string DownloadPersonalData => "DownloadPersonalData";

        public static string PersonalData => "PersonalData";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
