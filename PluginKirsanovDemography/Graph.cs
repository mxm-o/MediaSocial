// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using PhotoEdit;
using System;
using System.Drawing;

namespace PluginKirsanovDemography
{
    public class Graph
    {
        // Одна строка графика
        public Image RenderGraph(int value1, int value2)
        {
            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.height = 140;
            photo.width = 620;
            photo.colorMain = Color.White;
            Image imageOut = photo.Fill();
            // Рисуем нижний график на всю ширину
            photo.width = 350;
            photo.colorMain = value1 >= value2 ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
            Image image1 = photo.Fill();
            // Рисуем верхний график на % ширины
            Image image2 = null;
            if (value2 != 0)
            {
                try
                {
                    photo.width = value1 >= value2 ? (350 / value1 * value2) : (350 / value2 * value1);
                } catch
                {
                    photo.width = 1;
                }
                if (photo.width <= 0) photo.width = 1;
                photo.colorMain = value1 <= value2 ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
                image2 = photo.Fill();
            }
            // Объединяем слои
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = image1;
            imageOut = mergeImage.MergeImage(); // На фон помещаем размытую фотографию
            if (value2 != 0)
            {
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = image2;
                imageOut = mergeImage.MergeImage(); // На фон помещаем размытую фотографию
            }
            // Пишем цифры
            TextGenerate textAutor = new TextGenerate();
            textAutor.textString = value1.ToString();
            textAutor.rectH = 110;
            textAutor.rectW = 130;
            textAutor.rectX = 350;
            textAutor.rectY = 0;
            textAutor.source = imageOut;
            textAutor.fontSize = 60;
            textAutor.colorText = Color.Black;
            textAutor.stringAlignment = StringAlignment.Far;
            textAutor.lineAlignment = StringAlignment.Far;
            textAutor.fontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();
            // Пишем цифры
            textAutor.textString = ((value1 - value2) > 0 ? "+" : "") + (value1 - value2).ToString();
            textAutor.rectH = 105;
            textAutor.rectW = 135;
            textAutor.rectX = 475;
            textAutor.rectY = 0;
            textAutor.source = imageOut;
            textAutor.fontSize = 44;
            textAutor.colorText = Color.FromArgb(116,116,116);
            textAutor.stringAlignment = StringAlignment.Near;
            textAutor.lineAlignment = StringAlignment.Far;
            textAutor.fontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();

            return imageOut;
        }

        public Image RenderGraphBorn(int girlNow, int girlOld, int boyNow, int boyOld)
        {
            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.height = 140;
            photo.width = 620;
            photo.colorMain = Color.White;
            Image imageOut = photo.Fill();
            // Определяем максимальную ширину 
            int maxGirl = Math.Max(girlNow, girlOld);
            int maxBoy = Math.Max(boyNow, boyOld);
            int maxBorn = Math.Max(maxGirl, maxBoy);

            // Рисуем нижний график на всю ширину
            try
            {
                photo.width = 350 / maxBorn * (girlNow > girlOld ? girlNow : girlOld);
            } catch
            {
                photo.width = 1;
            }
            photo.height = 70;
            photo.colorMain = girlNow >= girlOld ? Color.FromArgb(215, 86, 156) : Color.FromArgb(220, 120, 173);
            Image imageGirlNow = photo.Fill();
            // Рисуем верхний график на % ширины
            Image imageGirlOld = null;
            if (girlOld != 0)
            {
                //photo.width = girlNow >= girlOld ? (350 / girlNow * girlOld) : (350 / girlOld * girlNow);
                photo.width = 350 / maxBorn * (girlNow <= girlOld ? girlNow : girlOld);
                if (photo.width <= 0) photo.width = 1;
                photo.colorMain = girlNow <= girlOld ? Color.FromArgb(215, 86, 156) : Color.FromArgb(220, 120, 173);
                imageGirlOld = photo.Fill();
            }
            // Рисуем нижний график на всю ширину
            try
            {
                photo.width = 350 / maxBorn * (boyNow > boyOld ? boyNow : boyOld);
            } catch
            {
                photo.width = 1;
            }
            photo.colorMain = boyNow >= boyOld ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
            Image imageBoyNow = photo.Fill();
            // Рисуем верхний график на % ширины
            Image imageBoyOld = null;
            if (girlOld != 0)
            {
                photo.width = boyNow >= boyOld ? (350 / boyNow * boyOld) : (350 / boyOld * boyNow);
                if (photo.width <= 0) photo.width = 1;
                photo.colorMain = boyNow <= boyOld ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
                imageBoyOld = photo.Fill();
            }
            // Объединяем слои
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = imageGirlNow;
            imageOut = mergeImage.MergeImage();
            if (boyOld != 0)
            {
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = imageGirlOld;
                imageOut = mergeImage.MergeImage(); 
            }
            mergeImage.sourceBottom = imageOut;
            mergeImage.top = 70;
            mergeImage.sourceTop = imageBoyNow;
            imageOut = mergeImage.MergeImage();
            if (boyOld != 0)
            {
                mergeImage.sourceBottom = imageOut;
                mergeImage.sourceTop = imageBoyOld;
                imageOut = mergeImage.MergeImage();
            }
            // Пишем цифры
            TextGenerate textGirlNow = new TextGenerate();
            textGirlNow.textString = girlNow.ToString();
            textGirlNow.rectH = 85;
            textGirlNow.rectW = 130;
            textGirlNow.rectX = 350;
            textGirlNow.rectY = 0;
            textGirlNow.source = imageOut;
            textGirlNow.fontSize = 60;
            textGirlNow.colorText = Color.Black;
            textGirlNow.stringAlignment = StringAlignment.Far;
            textGirlNow.lineAlignment = StringAlignment.Far;
            textGirlNow.fontName = "Lato";
            //textGirlNow.debug = true;
            imageOut = textGirlNow.DrawTextWithEffects();
            // Пишем цифры
            TextGenerate textGirlOld = new TextGenerate();
            textGirlOld.textString = ((girlNow - girlOld) > 0 ? "+" : "") + (girlNow - girlOld).ToString();
            textGirlOld.rectH = 80;
            textGirlOld.rectW = 135;
            textGirlOld.rectX = 475;
            textGirlOld.rectY = 0;
            textGirlOld.source = imageOut;
            textGirlOld.fontSize = 44;
            textGirlOld.colorText = Color.FromArgb(116, 116, 116);
            textGirlOld.stringAlignment = StringAlignment.Near;
            textGirlOld.lineAlignment = StringAlignment.Far;
            textGirlOld.fontName = "Lato";
            //textGirlOld.debug = true;
            imageOut = textGirlOld.DrawTextWithEffects();
            // Пишем цифры
            TextGenerate textBoyNow = new TextGenerate();
            textBoyNow.textString = boyNow.ToString();
            textBoyNow.rectH = 85;
            textBoyNow.rectW = 130;
            textBoyNow.rectX = 350;
            textBoyNow.rectY = 70;
            textBoyNow.source = imageOut;
            textBoyNow.fontSize = 60;
            textBoyNow.colorText = Color.Black;
            textBoyNow.stringAlignment = StringAlignment.Far;
            textBoyNow.lineAlignment = StringAlignment.Far;
            textBoyNow.fontName = "Lato";
            //textBoyNow.debug = true;
            imageOut = textBoyNow.DrawTextWithEffects();
            // Пишем цифры
            TextGenerate textBoyOld = new TextGenerate();
            textBoyOld.textString = ((boyNow - boyOld) > 0 ? "+" : "") + (boyNow - boyOld).ToString();
            textBoyOld.rectH = 80;
            textBoyOld.rectW = 135;
            textBoyOld.rectX = 475;
            textBoyOld.rectY = 70;
            textBoyOld.source = imageOut;
            textBoyOld.fontSize = 44;
            textBoyOld.colorText = Color.FromArgb(116, 116, 116);
            textBoyOld.stringAlignment = StringAlignment.Near;
            textBoyOld.lineAlignment = StringAlignment.Far;
            textBoyOld.fontName = "Lato";
            //textBoyOld.debug = true;
            imageOut = textBoyOld.DrawTextWithEffects();
            return imageOut;
        }
    }
}
