namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultExpressionTemplateService : IExpressionTemplateService
    {
        const string PrefixText = "<%";
        const string SuffixText = "%>";

        public bool IsExpression(string text)
        {
            return text != null && text.StartsWith(PrefixText, System.StringComparison.InvariantCulture) && text.EndsWith(SuffixText, System.StringComparison.InvariantCulture);
        }

        public string PickExpression(string text)
        {
            if (IsExpression(text))
            {
                return text?.Substring(PrefixText.Length, text.Length - PrefixText.Length - SuffixText.Length);
            }
            return null;
        }
    }
}
