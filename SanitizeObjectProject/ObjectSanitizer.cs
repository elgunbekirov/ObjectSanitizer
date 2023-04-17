using System.Text.RegularExpressions;

public static class ObjectSanitizer
{
    private static readonly Regex SanitizeRegex = new Regex("[^a-zA-Z0-9.]");

    public static void Sanitize(object obj)
    {
        var objType = obj.GetType();
        var props = objType.GetProperties();
        var sanitizedProps = new Dictionary<string, object>();
         
        foreach (var prop in props)
        {
            if (prop.PropertyType == typeof(string))
            {
                var value = prop.GetValue(obj) as string;
                if (value != null)
                {
                    var sanitizedValue = SanitizeRegex.Replace(value, "");
                    sanitizedProps[prop.Name] = sanitizedValue;
                }
            }
            else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(object))
            {
                var nestedObj = prop.GetValue(obj);
                Sanitize(nestedObj);
            }
        }

        if (sanitizedProps.Count > 0)
        {
            Console.WriteLine($"Sanitized {objType.Name}:");
            foreach (var kvp in sanitizedProps)
            {
                Console.WriteLine($"\t{kvp.Key}: {kvp.Value}");
            }
        }
    }

    static void Main(string[] args)
    {
        Payment payment = new Payment
        {
            Amount = new AmountInfo { Amount = 100, Currency = "USD!" },
            PaymentDate = new DateTime(2022, 1, 1),
            Message = "Payment@ from Tip@alti",
            RefCode = "Abc123$"
        };

        ObjectSanitizer.Sanitize(payment);
    }


}

class Payment
{
    public AmountInfo Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Message { get; set; }
    public string RefCode { get; set; }
    public Payment otherPayment;
}

class AmountInfo
{
    public double Amount { get; set; }
    public string Currency { get; set; }
}

