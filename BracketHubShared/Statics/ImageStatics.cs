using BracketHubShared.Extensions;

namespace BracketHubShared.Statics
{
    public static class ImageStatics
    {
        public static string CoverUrl(string? type) => $"images/covers/{type ?? "default"}-cover.jpg";
        public static string IconUrl(string? type) => $"images/icons/{type ?? "default"}-icon.jpg";
        public static string BackgroundUrl(string? type) => $"images/backgrounds/{type ?? "default"}-background.jpg";
        public static string BannerUrl(string? type) => $"images/banners/{type ?? "default"}-banner.jpg";

        public static string? GetCustomOrExistingBanner(string? url)
        {
            if (url.IsNotNull() && url.StartsWith("banner:"))
            {
                return BannerUrl(url.Substring(6, url.Length));
            }
            else
            {
                return url;
            }
        }
    }
}
