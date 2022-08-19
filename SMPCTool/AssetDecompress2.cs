using System;
using System.IO;

namespace SMPCTool
{
	// Token: 0x02000005 RID: 5
	public class AssetDecompress2
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000027BC File Offset: 0x000009BC
		public AssetDecompress2(byte[] compressedBytes, string fileName, int size, int compressedIndex = 0)
		{
			this.com = compressedBytes;
			this.com_i = compressedIndex;
			this.dec_size = size;
			this.dec = new byte[this.dec_size];
			while (this.dec_i <= this.dec_size && this.com_i < this.com.Length)
			{
				this.token_a = (int)this.com[this.com_i];
				this.com_i++;
				bool flag = !this.backref_turn;
				if (flag)
				{
					bool flag2 = (this.token_a & 240) == 240;
					if (flag2)
					{
						this.token_b = (int)this.com[this.com_i];
						this.com_i++;
					}
					else
					{
						this.token_b = 0;
					}
				}
				else
				{
					this.token_b = (int)this.com[this.com_i];
					this.com_i++;
				}
				bool flag3 = !this.backref_turn;
				if (flag3)
				{
					this.literal_len = (this.token_a >> 4) + this.token_b;
					while (this.literal_len >= 270 && (this.literal_len - 15) % 255 == 0)
					{
						this.literal_len += (int)this.com[this.com_i];
						this.com_i++;
						bool flag4 = this.com[this.com_i - 1] == 0;
						if (flag4)
						{
							break;
						}
					}
					int num = 0;
					for (int i = this.dec_i; i < this.dec_i + this.literal_len; i++)
					{
						try
						{
							this.dec[i] = this.com[this.com_i + num];
							num++;
						}
						catch
						{
						}
					}
					this.com_i += this.literal_len;
					this.dec_i += this.literal_len;
					this.backref_len = (this.token_a & 15) + 4;
				}
				else
				{
					this.backref_dis = this.token_a + (this.token_b << 8);
					bool flag5 = this.backref_len == 19;
					if (flag5)
					{
						this.backref_len += (int)this.com[this.com_i];
						this.com_i++;
						while (this.backref_len >= 274 && (this.backref_len - 19) % 255 == 0)
						{
							this.backref_len += (int)this.com[this.com_i];
							this.com_i++;
							bool flag6 = this.com[this.com_i - 1] == 0;
							if (flag6)
							{
								break;
							}
						}
					}
					for (int j = 0; j < this.backref_len; j++)
					{
						try
						{
							this.dec[this.dec_i + j] = this.dec[this.dec_i - this.backref_dis + j];
						}
						catch
						{
						}
					}
					this.dec_i += this.backref_len;
				}
				this.backref_turn = !this.backref_turn;
			}
			File.WriteAllBytes(fileName, this.dec);
		}

		// Token: 0x04000004 RID: 4
		private byte[] com;

		// Token: 0x04000005 RID: 5
		private byte[] dec;

		// Token: 0x04000006 RID: 6
		private int dec_size;

		// Token: 0x04000007 RID: 7
		private int dec_i = 0;

		// Token: 0x04000008 RID: 8
		private int com_i = 36;

		// Token: 0x04000009 RID: 9
		private int token_a;

		// Token: 0x0400000A RID: 10
		private int token_b;

		// Token: 0x0400000B RID: 11
		private bool backref_turn = false;

		// Token: 0x0400000C RID: 12
		private int literal_len;

		// Token: 0x0400000D RID: 13
		private int backref_dis;

		// Token: 0x0400000E RID: 14
		private int backref_len;

		// Token: 0x0400000F RID: 15
		private int next_small;
	}
}
