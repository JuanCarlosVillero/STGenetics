
namespace STGenetics.Domain.ErrorHandling
{
    using System.Globalization;

    public static class MessageHandler
    {
        public static class MessageCodes
        {
            public static string ORDER_HAS_NO_ORDERLINE_VALIDATION
            {
                get { return "1000"; }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public static string GetErrorDescription(string messageCode)
        {
            return MessageResource.ResourceManager.GetString(messageCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetErrorDescription(string messageCode, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture, MessageResource.ResourceManager.GetString(messageCode), parameters);
        }
    }
}
