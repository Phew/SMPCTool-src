using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Ionic.Zlib;

namespace SMPCTool
{
	// Token: 0x02000007 RID: 7
	public class DAG
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000038E0 File Offset: 0x00001AE0
		public DAG(string fileName)
		{
			this.Filename = fileName;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000038FC File Offset: 0x00001AFC
		public void DecompressDAG(string fileName)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.Filename));
			uint num = binaryReader.ReadUInt32();
			bool flag = num != 2300540847U;
			if (flag)
			{
				MessageBox.Show("Invalid DAG file", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				int count = binaryReader.ReadInt32();
				binaryReader.ReadInt32();
				byte[] bytes = ZlibStream.UncompressBuffer(binaryReader.ReadBytes(count));
				File.WriteAllBytes(fileName, bytes);
				this.decompressedFileName = fileName;
				binaryReader.Close();
				binaryReader.Dispose();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003984 File Offset: 0x00001B84
		private ulong StringToHash(string text)
		{
			Process process = new Process();
			process.StartInfo.FileName = "SMPS4HashTool.exe";
			process.StartInfo.Arguments = "\"" + text + "\"";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.Start();
			ulong hash = 0UL;
			process.OutputDataReceived += delegate(object sender2, DataReceivedEventArgs e2)
			{
				string data = e2.Data;
				bool flag = string.IsNullOrWhiteSpace(data);
				if (!flag)
				{
					bool flag2 = data.Contains(" ");
					if (!flag2)
					{
						hash = ulong.Parse(data);
					}
				}
			};
			process.BeginOutputReadLine();
			process.WaitForExit();
			return hash;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003A2C File Offset: 0x00001C2C
		public void ParseDecompressedDAG()
		{
			bool flag = !File.Exists(this.decompressedFileName);
			if (!flag)
			{
				this.Dependencies.Clear();
				BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
				binaryReader.BaseStream.Position = 102L;
				long position = binaryReader.BaseStream.Position;
				for (;;)
				{
					bool flag2 = binaryReader.ReadByte() == 0;
					if (flag2)
					{
						long num = binaryReader.BaseStream.Position - 1L;
						binaryReader.BaseStream.Position = position;
						byte[] bytes = binaryReader.ReadBytes((int)(num - position));
						string @string = Encoding.ASCII.GetString(bytes);
						Console.WriteLine(@string);
						this.Dependencies.Add(@string);
						binaryReader.BaseStream.Position += 1L;
						position = binaryReader.BaseStream.Position;
						bool flag3 = binaryReader.ReadByte() == 0;
						if (flag3)
						{
							break;
						}
					}
				}
				foreach (string text in this.Dependencies)
				{
					MessageBox.Show(this.StringToHash(text).ToString("X2"));
				}
				File.WriteAllLines(Globals.TemporaryDirectory + "DependencyDag.txt", this.Dependencies);
				binaryReader.Close();
				binaryReader.Dispose();
			}
		}

		// Token: 0x0400001E RID: 30
		public string Filename;

		// Token: 0x0400001F RID: 31
		public List<string> Dependencies = new List<string>();

		// Token: 0x04000020 RID: 32
		private const uint stringStartOffset = 102U;

		// Token: 0x04000021 RID: 33
		private const uint headerMagic = 2300540847U;

		// Token: 0x04000022 RID: 34
		private string decompressedFileName;
	}
}
