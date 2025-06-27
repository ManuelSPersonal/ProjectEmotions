namespace emotionsAPI.Models
{
    public class Emotions
    {
        public enum Emotion
        {
            positivo,
            negativo,
            neutral
        }

        public static readonly string[] Positivo =
        {
            "excelente", "genial", "fantástico", "bueno", "increíble"
        };

        public static readonly string[] Negativo =
        {
            "malo", "terrible", "problema", "defecto", "horrible"
        };
    }
}
