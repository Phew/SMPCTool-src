using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000014 RID: 20
	public partial class TextureToolForm : Form
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000C2F5 File Offset: 0x0000A4F5
		public TextureToolForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000C318 File Offset: 0x0000A518
		private void openTextureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Open Texture Asset...";
			string text = ".texture";
			openFileDialog.Filter = "(*" + text + ")| *" + text;
			openFileDialog.RestoreDirectory = true;
			bool flag = openFileDialog.ShowDialog() != DialogResult.OK;
			if (!flag)
			{
				BinaryReader binaryReader = new BinaryReader(File.OpenRead(openFileDialog.FileName));
				uint num = binaryReader.ReadUInt32();
				bool flag2 = num != 1548058809U;
				if (flag2)
				{
					MessageBox.Show("Invalid Texture Asset!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					binaryReader.Close();
					binaryReader.Dispose();
				}
				else
				{
					binaryReader.BaseStream.Position = 88L;
					uint num2 = binaryReader.ReadUInt32();
					Console.WriteLine("secondFileSize: " + num2.ToString());
					ushort num3 = binaryReader.ReadUInt16();
					ushort num4 = binaryReader.ReadUInt16();
					Console.WriteLine(num3.ToString() + "x" + num4.ToString());
					binaryReader.BaseStream.Position = 104L;
					byte b = binaryReader.ReadByte();
					binaryReader.BaseStream.Position = 114L;
					byte b2 = binaryReader.ReadByte();
					int num5 = (int)num3;
					int num6 = (int)num4;
					binaryReader.BaseStream.Position = 128L;
					foreach (string path in Directory.GetFiles(Globals.TemporaryDirectory))
					{
						bool flag3 = Path.GetFileName(path).StartsWith("mipmap");
						if (flag3)
						{
							File.Delete(path);
						}
					}
					this.textureDatas.Clear();
					long num7 = 0L;
					for (int j = 0; j < (int)b2; j++)
					{
						TextureToolForm.TextureData textureData = new TextureToolForm.TextureData();
						textureData.MipMap = j;
						textureData.FileName = openFileDialog.FileName;
						textureData.Type = (int)b;
						Console.WriteLine("Mip Map " + j.ToString());
						Console.WriteLine("Mip Map Size: " + num5.ToString() + "x" + num6.ToString());
						int num8 = num5 * num6 / 2;
						bool flag4 = b == 99 || b == 80;
						if (flag4)
						{
							num8 = num5 * num6;
						}
						textureData.Size = num8;
						textureData.Width = num5;
						textureData.Height = num6;
						bool flag5 = num2 > 0U && num7 < (long)((ulong)num2);
						if (flag5)
						{
							string text2 = openFileDialog.FileName;
							text2 = openFileDialog.FileName.Replace(".texture", ".2.texture");
							BinaryReader binaryReader2 = new BinaryReader(File.OpenRead(text2));
							textureData.FileName = text2;
							binaryReader2.BaseStream.Position = num7;
							textureData.Offset = (int)binaryReader2.BaseStream.Position;
							byte[] array = binaryReader2.ReadBytes(num8);
							num7 = binaryReader2.BaseStream.Position;
							File.WriteAllBytes(string.Concat(new string[]
							{
								Globals.TemporaryDirectory,
								"mipmap-",
								j.ToString(),
								"-",
								num5.ToString(),
								"x",
								num6.ToString()
							}), array);
							List<byte> list = new List<byte>
							{
								68,
								68,
								83,
								32,
								124,
								0,
								0,
								0,
								7,
								16,
								0,
								0
							};
							list.AddRange(BitConverter.GetBytes(num6));
							list.AddRange(BitConverter.GetBytes(num5));
							list.AddRange(BitConverter.GetBytes(num8));
							list.AddRange(new List<byte>
							{
								0,
								0,
								0,
								0,
								1,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								32,
								0,
								0,
								0,
								4,
								0,
								0,
								0,
								68,
								88,
								84,
								49,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0
							});
							bool flag6 = b == 99 || b == 80;
							if (flag6)
							{
								list[22] = 64;
								list[86] = 49;
								list[87] = 48;
								list.AddRange(new List<byte>
								{
									98,
									0,
									0,
									0,
									3,
									0,
									0,
									0,
									0,
									0,
									0,
									0,
									1,
									0,
									0,
									0,
									0,
									0,
									0,
									0
								});
								list.AddRange(array);
							}
							else
							{
								list.AddRange(array);
							}
							File.WriteAllBytes(string.Concat(new string[]
							{
								Globals.TemporaryDirectory,
								"mipmap-",
								j.ToString(),
								"-",
								num5.ToString(),
								"x",
								num6.ToString(),
								".dds"
							}), list.ToArray());
							num5 /= 2;
							num6 /= 2;
							binaryReader2.Close();
							binaryReader2.Dispose();
							this.textureDatas.Add(textureData);
						}
						else
						{
							textureData.Offset = (int)binaryReader.BaseStream.Position;
							byte[] array2 = binaryReader.ReadBytes(num8);
							File.WriteAllBytes(string.Concat(new string[]
							{
								Globals.TemporaryDirectory,
								"mipmap-",
								j.ToString(),
								"-",
								num5.ToString(),
								"x",
								num6.ToString()
							}), array2);
							List<byte> list2 = new List<byte>
							{
								68,
								68,
								83,
								32,
								124,
								0,
								0,
								0,
								7,
								16,
								0,
								0
							};
							list2.AddRange(BitConverter.GetBytes(num6));
							list2.AddRange(BitConverter.GetBytes(num5));
							list2.AddRange(BitConverter.GetBytes(num8));
							list2.AddRange(new List<byte>
							{
								0,
								0,
								0,
								0,
								1,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								32,
								0,
								0,
								0,
								4,
								0,
								0,
								0,
								68,
								88,
								84,
								49,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0
							});
							bool flag7 = b == 99 || b == 80;
							if (flag7)
							{
								list2[22] = 64;
								list2[86] = 49;
								list2[87] = 48;
								list2.AddRange(new List<byte>
								{
									98,
									0,
									0,
									0,
									3,
									0,
									0,
									0,
									0,
									0,
									0,
									0,
									1,
									0,
									0,
									0,
									0,
									0,
									0,
									0
								});
								list2.AddRange(array2);
							}
							list2.AddRange(array2);
							File.WriteAllBytes(string.Concat(new string[]
							{
								Globals.TemporaryDirectory,
								"mipmap-",
								j.ToString(),
								"-",
								num5.ToString(),
								"x",
								num6.ToString(),
								".dds"
							}), list2.ToArray());
							num5 /= 2;
							num6 /= 2;
							this.textureDatas.Add(textureData);
						}
					}
					binaryReader.Close();
					binaryReader.Dispose();
					Console.WriteLine("Done Opening Texture Assets");
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		private void saveTextureAssetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.textureDatas.Count <= 0;
			if (!flag)
			{
				for (int i = 0; i < this.textureDatas.Count; i++)
				{
					TextureToolForm.TextureData textureData = this.textureDatas[i];
					BinaryReader binaryReader = new BinaryReader(File.OpenRead(string.Concat(new string[]
					{
						Globals.TemporaryDirectory,
						"mipmap-",
						textureData.MipMap.ToString(),
						"-",
						textureData.Width.ToString(),
						"x",
						textureData.Height.ToString(),
						".dds"
					})));
					binaryReader.BaseStream.Position = 128L;
					bool flag2 = textureData.Type == 99 || textureData.Type == 80;
					if (flag2)
					{
						binaryReader.BaseStream.Position += 20L;
					}
					byte[] buffer = binaryReader.ReadBytes(textureData.Size);
					binaryReader.Close();
					binaryReader.Dispose();
					Console.WriteLine("writing to " + textureData.FileName);
					BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(textureData.FileName));
					binaryWriter.BaseStream.Position = (long)textureData.Offset;
					binaryWriter.Write(buffer);
					binaryWriter.Close();
					binaryWriter.Dispose();
				}
				Console.WriteLine("Done Saving Texture Assets");
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000D34D File Offset: 0x0000B54D
		private void TextureToolForm_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x04000089 RID: 137
		private List<TextureToolForm.TextureData> textureDatas = new List<TextureToolForm.TextureData>();

		// Token: 0x0200002B RID: 43
		public class TextureData
		{
			// Token: 0x040000E0 RID: 224
			public string FileName;

			// Token: 0x040000E1 RID: 225
			public int MipMap;

			// Token: 0x040000E2 RID: 226
			public int Offset;

			// Token: 0x040000E3 RID: 227
			public int Size;

			// Token: 0x040000E4 RID: 228
			public int Width;

			// Token: 0x040000E5 RID: 229
			public int Height;

			// Token: 0x040000E6 RID: 230
			public int Type;
		}
	}
}
