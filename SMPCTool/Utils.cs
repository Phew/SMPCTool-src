using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

// Token: 0x02000003 RID: 3
public static class Utils
{
	// Token: 0x06000007 RID: 7 RVA: 0x000021F0 File Offset: 0x000003F0
	public static void ClearRows(this ListView lv)
	{
		while (lv.Items.Count > 1)
		{
			lv.Items.RemoveAt(1);
		}
		bool flag = lv.Items.Count > 0;
		if (flag)
		{
			lv.Items.RemoveAt(0);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002244 File Offset: 0x00000444
	public static string ReadNTString(this BinaryReader br)
	{
		string text = "";
		for (;;)
		{
			byte b = br.ReadByte();
			bool flag = b == 0;
			if (flag)
			{
				break;
			}
			string str = text;
			char c = (char)b;
			text = str + c.ToString();
		}
		return text;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000228C File Offset: 0x0000048C
	public static string ReadStringWithLen(this BinaryReader br, int len)
	{
		string text = "";
		for (int i = 0; i < len; i++)
		{
			text += ((char)br.ReadByte()).ToString();
		}
		return text;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000022D0 File Offset: 0x000004D0
	public static Process RunProcessWithArgs(string fileName, string args)
	{
		Process process = new Process();
		process.StartInfo.FileName = fileName;
		process.StartInfo.Arguments = args;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;
		process.Start();
		process.WaitForExit();
		return process;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000232C File Offset: 0x0000052C
	public static List<int> FindOffsetPattern(byte[] bytes, byte[] pattern, int startIndex = 0, int max = -1, int endOffset = -1, bool skipPatternLength = false)
	{
		List<int> list = new List<int>();
		int num = Array.IndexOf<byte>(bytes, pattern[0], startIndex);
		while (num >= 0 && num <= bytes.Length - pattern.Length)
		{
			byte[] array = new byte[pattern.Length];
			Buffer.BlockCopy(bytes, num, array, 0, pattern.Length);
			bool flag = array.SequenceEqual(pattern);
			if (flag)
			{
				list.Add(num);
				bool flag2 = max != -1;
				if (flag2)
				{
					bool flag3 = list.Count >= max;
					if (flag3)
					{
						return list;
					}
				}
			}
			num = Array.IndexOf<byte>(bytes, pattern[0], skipPatternLength ? (num + pattern.Length) : (num + 1));
			bool flag4 = endOffset != -1 && num > endOffset;
			if (!flag4)
			{
				continue;
			}
			return list;
		}
		return list;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000023FC File Offset: 0x000005FC
	public static string ReadStringBytes(byte[] bytes, int startIndex, int length = -1)
	{
		string text = "";
		bool flag = length == -1;
		if (flag)
		{
			while (bytes[startIndex] > 0)
			{
				string str = text;
				char c = (char)bytes[startIndex];
				text = str + c.ToString();
				startIndex++;
			}
		}
		else
		{
			for (int i = 0; i < length; i++)
			{
				string str2 = text;
				char c = (char)bytes[startIndex];
				text = str2 + c.ToString();
				startIndex++;
			}
		}
		return text;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000247C File Offset: 0x0000067C
	public static void DeleteDirectoryTotally(string dir)
	{
		foreach (string path in Directory.GetFiles(dir))
		{
			File.Delete(path);
		}
		foreach (string dir2 in Directory.GetDirectories(dir))
		{
			Utils.DeleteDirectoryTotally(dir2);
		}
		Thread.Sleep(100);
		Directory.Delete(dir);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000024E8 File Offset: 0x000006E8
	public static int ReadInt16(byte[] bytes, int startIndex, bool bigEndian = false)
	{
		byte[] array = new byte[2];
		int num = startIndex;
		for (int i = 0; i < 2; i++)
		{
			array[i] = bytes[num];
			num++;
		}
		if (bigEndian)
		{
			Array.Reverse(array);
		}
		return (int)BitConverter.ToInt16(array, 0);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002538 File Offset: 0x00000738
	public static int ReadInt32(byte[] bytes, int startIndex, bool bigEndian = false)
	{
		byte[] array = new byte[4];
		int num = startIndex;
		for (int i = 0; i < 4; i++)
		{
			array[i] = bytes[num];
			num++;
		}
		if (bigEndian)
		{
			Array.Reverse(array);
		}
		return BitConverter.ToInt32(array, 0);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002588 File Offset: 0x00000788
	public static uint ReadUInt32(byte[] bytes, int startIndex, bool bigEndian = false)
	{
		byte[] array = new byte[4];
		int num = startIndex;
		for (int i = 0; i < 4; i++)
		{
			array[i] = bytes[num];
			num++;
		}
		if (bigEndian)
		{
			Array.Reverse(array);
		}
		return BitConverter.ToUInt32(array, 0);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000025D8 File Offset: 0x000007D8
	public static ushort ReadUInt16(byte[] bytes, int startIndex, bool bigEndian = false)
	{
		byte[] array = new byte[2];
		int num = startIndex;
		for (int i = 0; i < 2; i++)
		{
			array[i] = bytes[num];
			num++;
		}
		if (bigEndian)
		{
			Array.Reverse(array);
		}
		return BitConverter.ToUInt16(array, 0);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002628 File Offset: 0x00000828
	public static void WriteInt32(byte[] bytes, int startIndex, int value, bool bigEndian = false)
	{
		int num = startIndex;
		byte[] bytes2 = BitConverter.GetBytes(value);
		if (bigEndian)
		{
			Array.Reverse(bytes2);
		}
		for (int i = 0; i < 4; i++)
		{
			bytes[num] = bytes2[i];
			num++;
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000266C File Offset: 0x0000086C
	public static void WriteUInt32(byte[] bytes, int startIndex, uint value, bool bigEndian = false)
	{
		int num = startIndex;
		byte[] bytes2 = BitConverter.GetBytes(value);
		if (bigEndian)
		{
			Array.Reverse(bytes2);
		}
		for (int i = 0; i < 4; i++)
		{
			bytes[num] = bytes2[i];
			num++;
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000026B0 File Offset: 0x000008B0
	public static int ReadByte(byte[] bytes, int startIndex)
	{
		return (int)bytes[startIndex];
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000026C8 File Offset: 0x000008C8
	public static byte[] ReadBytes(byte[] bytes, int startIndex, int length)
	{
		byte[] array = new byte[length];
		int num = startIndex;
		for (int i = 0; i < length; i++)
		{
			array[i] = bytes[num];
			num++;
		}
		return array;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002704 File Offset: 0x00000904
	public static byte[] ToLiteralBytes(this string text)
	{
		byte[] array = new byte[text.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
		}
		return array;
	}
}
