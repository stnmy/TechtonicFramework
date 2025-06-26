using System;
using System.IO;

namespace TechtonicFramework.Extensions
{
    public static class HelperMethods
    {
        public static string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return name.ToLower().Replace(" ", "-");
        }

        public static string GetRelativeImagePath(string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath)) return inputPath;

            var fileName = Path.GetFileName(inputPath.Replace("\\", "/"));
            return "/images/products/" + fileName;
        }
    }
}
