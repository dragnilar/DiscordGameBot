using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoogunkBot.Modules
{
    public class AmaModule
    {
        public string ConvertAmaString(string amaString)
        {
            var lowerString = amaString.ToLower();
            var response = string.Empty;
            var endAmaPosition = lowerString.LastIndexOf("ama");
            if (endAmaPosition < 0)
            {
                return "Do you want me to ask you anything?";
            }

            lowerString = lowerString.Substring(0, endAmaPosition) + "can I ask you anything?";

            var hasIamResult = HasImString(lowerString);
            if (hasIamResult.hasIm)
            {
                var position = lowerString.IndexOf(hasIamResult.imString);
                if (position >= 0)
                {
                    var replacement = lowerString.Substring(0, position) + "Are you" +
                                      lowerString.Substring(position + hasIamResult.imString.Length);
                    response = replacement;
                }
            }
            else
            {
                response = "Do you " + lowerString;
            }

            return response;

        }

        private (bool hasIm, string imString) HasImString(string lowerAmaString)
        {
            if (lowerAmaString.StartsWith("i'm"))
            {
                return (true, "i'm");
            }

            if (lowerAmaString.StartsWith("i am"))
            {
                return (true, "i am");
            }

            if (lowerAmaString.StartsWith("im"))
            {
                return (true, "im");
            }

            return (false, string.Empty);
        }

    }
}
