namespace SmsService.Core.Model
{
    public class Config: Model
    {
        public override string ToString()
          => Name;

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
