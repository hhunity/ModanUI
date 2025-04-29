using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageMagick;
using System;

namespace TiffDotTests
{
    [TestClass]
    public class DotEmbeddingTests
    {
        [TestMethod]
        public void RandomBitPattern_ShouldMatchTiffPixels()
        {
            // 1. テストデータ生成（ushort[100]）
            ushort[] inputs = new ushort[100];
            Random rand = new Random(42);  // 再現性あり
            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = (ushort)rand.Next(ushort.MinValue, ushort.MaxValue);

            // 2. TIFF画像作成設定（40 x 40, 上→下, 右に進む）
            const int width = 40, height = 40;
            var image = new MagickImage(MagickColors.White, width, height);
            image.Depth = 1;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int bit = 0; bit < 16; bit++)
                {
                    bool isBlack = ((inputs[i] >> (15 - bit)) & 1) == 1;

                    int pixelIndex = i * 16 + bit;
                    int x = pixelIndex / height;  // 縦優先
                    int y = pixelIndex % height;

                    if (isBlack)
                        image.GetPixels().SetPixel(x, y, new MagickColor(0, 0, 0));
                }
            }

            // 3. 保存
            string filePath = "test_output.tif";
            image.Write(filePath, MagickFormat.Tiff);

            // 4. 再読み込みして検証
            using var loaded = new MagickImage(filePath);
            var pixels = loaded.GetPixels();

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int bit = 0; bit < 16; bit++)
                {
                    bool expectedBlack = ((inputs[i] >> (15 - bit)) & 1) == 1;

                    int pixelIndex = i * 16 + bit;
                    int x = pixelIndex / height;
                    int y = pixelIndex % height;

                    byte actualRed = pixels.GetPixel(x, y).GetChannel(0);
                    bool actualBlack = actualRed < 128;  // 中間値より小さければ黒とみなす

                    Assert.AreEqual(expectedBlack, actualBlack,
                        $"Pixel mismatch at ({x},{y}): expected {(expectedBlack ? "black" : "white")}, actual {(actualBlack ? "black" : "white")}");
                }
            }
        }

[DataTestMethod]
[DynamicData(nameof(TestCases), DynamicDataSourceType.Method)]
public void TestDotEmbedding(int x, int y, bool shouldBeBlack)
{
    var image = new TiffImage(100, 100);
    image.Fill(White);

    image.SetPixel(10, 20, Black);
    image.SetPixel(30, 40, Black);

    image.Save("test.tif");
    var loaded = new TiffImage("test.tif");

    var actual = loaded.GetPixel(x, y) == Black;
    Assert.AreEqual(shouldBeBlack, actual);
}

public static IEnumerable<object[]> TestCases()
{
    yield return new object[] { 0, 0, false };
    yield return new object[] { 10, 20, true };
    yield return new object[] { 30, 40, true };
    // ここに100個くらいズラズラ並べる
}

[TestClass]
public class TiffDotTests
{
    [DataTestMethod]
    [DataRow(10, 20, true)]
    [DataRow(30, 40, true)]
    [DataRow(0, 0, false)]  // 打ってないので白
    public void TestDotEmbedding(int x, int y, bool shouldBeBlack)
    {
        var image = new TiffImage(100, 100);
        image.Fill(White);

        image.SetPixel(10, 20, Black);
        image.SetPixel(30, 40, Black);

        image.Save("test.tif");
        var loaded = new TiffImage("test.tif");

        var actual = loaded.GetPixel(x, y) == Black;
        Assert.AreEqual(shouldBeBlack, actual);
    }
}
    }
}