using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class ColorArrayConverter : JsonConverter<Color[,]>
    {
        public override bool CanConvert(System.Type typeToConvert)
        {
            return typeToConvert.IsArray && typeToConvert.GetElementType() == typeof(Color);
        }

        public override Color[,] Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var rows = new List<Color[]>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                var columns = new List<Color>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        break;
                    }

                    if (reader.TokenType != JsonTokenType.Number)
                    {
                        throw new JsonException();
                    }

                    var colorValue = reader.GetInt32();
                    var color = (Color)Enum.ToObject(typeof(Color), colorValue);
                    columns.Add(color);
                }

                rows.Add(columns.ToArray());
            }

            var board = new Color[rows.Count, rows[0].Length];
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    board[i, j] = rows[i][j];
                }
            }

            return board;
        }

        public override void Write(Utf8JsonWriter writer, Color[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            for (int i = 0; i < value.GetLength(0); i++)
            {
                writer.WriteStartArray();
                for (int j = 0; j < value.GetLength(1); j++)
                {
                    writer.WriteNumberValue((int)value[i, j]);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }
}