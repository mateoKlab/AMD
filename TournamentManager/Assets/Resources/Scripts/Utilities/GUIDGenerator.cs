using System;
using System.Collections;
using System.Text;

public static class GUIDGenerator {

    /// <summary>
    /// Generates a Global Unique Identifier.
    /// </summary>
    public static string NewGuid() {
        return Guid.NewGuid().ToString();
        Random random = new Random();
        StringBuilder guidBuilder = new StringBuilder(8 + 4 + 4 + 4 + 12 + 4);
        AppendRandomInt(random, guidBuilder);
        guidBuilder.Append('-');
        AppendRandomShort(random, guidBuilder);
        guidBuilder.Append('-');
        AppendRandomShort(random, guidBuilder);
        guidBuilder.Append('-');
        AppendRandomShort(random, guidBuilder);
        guidBuilder.Append('-');
        AppendRandomInt(random, guidBuilder);
        AppendRandomShort(random, guidBuilder);
        
        return guidBuilder.ToString();
    }
    
    private static void AppendRandomInt(Random random, StringBuilder stringBuilder)
    {
        int randomInt = random.Next();
        ByteAppendToStringBuilderInHex((byte) (randomInt >> 24), stringBuilder);
        ByteAppendToStringBuilderInHex((byte) (randomInt >> 16), stringBuilder);
        ByteAppendToStringBuilderInHex((byte) (randomInt >> 8), stringBuilder);
        ByteAppendToStringBuilderInHex((byte) randomInt, stringBuilder);
    }
    
    private static void AppendRandomShort(Random random, StringBuilder stringBuilder)
    {
        int randomInt = random.Next();
        ByteAppendToStringBuilderInHex((byte) (randomInt >> 8), stringBuilder);
        ByteAppendToStringBuilderInHex((byte) randomInt, stringBuilder);
    }
    
    private static void ByteAppendToStringBuilderInHex(byte value, StringBuilder stringBuilder)
    {
        byte b;
        b = (byte)(value >> 4);
        stringBuilder.Append((char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30));
        
        b = (byte)(value & 0x0F);
        stringBuilder.Append((char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30));
    }
}
