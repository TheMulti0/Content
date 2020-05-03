namespace Galatz.News
{
    public static class GalatzConstants
    {
        private const string BaseAddress = "https://glz.co.il";

        public static string ToAbsoluteUrl(string relativeUrl)
            => $"{BaseAddress}{relativeUrl}";
    }
}