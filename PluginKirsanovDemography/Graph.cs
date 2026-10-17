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
            photo.Height = 140;
            photo.Width = 620;
            photo.ColorMain = Color.White;
            Image imageOut = photo.Fill();
            // Рисуем нижний график на всю ширину
            photo.Width = 350;
            photo.ColorMain = value1 >= value2 ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
            Image image1 = photo.Fill();
            // Рисуем верхний график на % ширины
            Image image2 = null;
            if (value2 != 0)
            {
                try
                {
                    photo.Width = value1 >= value2 ? (350 / value1 * value2) : (350 / value2 * value1);
                }
                catch
                {
                    photo.Width = 1;
                }
                if (photo.Width <= 0) photo.Width = 1;
                photo.ColorMain = value1 <= value2 ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
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
            textAutor.TextString = value1.ToString();
            textAutor.RectH = 110;
            textAutor.RectW = 130;
            textAutor.RectX = 350;
            textAutor.RectY = 0;
            textAutor.Source = imageOut;
            textAutor.FontSize = 60;
            textAutor.ColorText = Color.Black;
            textAutor.StringAlignment = StringAlignment.Far;
            textAutor.LineAlignment = StringAlignment.Far;
            textAutor.FontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();
            // Пишем цифры
            textAutor.TextString = ((value1 - value2) > 0 ? "+" : "") + (value1 - value2).ToString();
            textAutor.RectH = 105;
            textAutor.RectW = 135;
            textAutor.RectX = 475;
            textAutor.RectY = 0;
            textAutor.Source = imageOut;
            textAutor.FontSize = 44;
            textAutor.ColorText = Color.FromArgb(116, 116, 116);
            textAutor.StringAlignment = StringAlignment.Near;
            textAutor.LineAlignment = StringAlignment.Far;
            textAutor.FontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();

            return imageOut;
        }

        public Image RenderGraphBorn(int girlNow, int girlOld, int boyNow, int boyOld)
        {
            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.Height = 140;
            photo.Width = 620;
            photo.ColorMain = Color.White;
            Image imageOut = photo.Fill();
            // Определяем максимальную ширину 
            int maxGirl = Math.Max(girlNow, girlOld);
            int maxBoy = Math.Max(boyNow, boyOld);
            int maxBorn = Math.Max(maxGirl, maxBoy);

            // Рисуем нижний график на всю ширину
            try
            {
                photo.Width = 350 / maxBorn * (girlNow > girlOld ? girlNow : girlOld);
            }
            catch
            {
                photo.Width = 1;
            }
            photo.Height = 70;
            photo.ColorMain = girlNow >= girlOld ? Color.FromArgb(215, 86, 156) : Color.FromArgb(220, 120, 173);
            Image imageGirlNow = photo.Fill();
            // Рисуем верхний график на % ширины
            Image imageGirlOld = null;
            if (girlOld != 0)
            {
                //photo.width = girlNow >= girlOld ? (350 / girlNow * girlOld) : (350 / girlOld * girlNow);
                photo.Width = 350 / maxBorn * (girlNow <= girlOld ? girlNow : girlOld);
                if (photo.Width <= 0) photo.Width = 1;
                photo.ColorMain = girlNow <= girlOld ? Color.FromArgb(215, 86, 156) : Color.FromArgb(220, 120, 173);
                imageGirlOld = photo.Fill();
            }
            // Рисуем нижний график на всю ширину
            try
            {
                photo.Width = 350 / maxBorn * (boyNow > boyOld ? boyNow : boyOld);
            }
            catch
            {
                photo.Width = 1;
            }
            photo.ColorMain = boyNow >= boyOld ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
            Image imageBoyNow = photo.Fill();
            // Рисуем верхний график на % ширины
            Image imageBoyOld = null;
            if (girlOld != 0)
            {
                photo.Width = boyNow >= boyOld ? (350 / boyNow * boyOld) : (350 / boyOld * boyNow);
                if (photo.Width <= 0) photo.Width = 1;
                photo.ColorMain = boyNow <= boyOld ? Color.FromArgb(50, 141, 212) : Color.FromArgb(62, 181, 241);
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
            textGirlNow.TextString = girlNow.ToString();
            textGirlNow.RectH = 85;
            textGirlNow.RectW = 130;
            textGirlNow.RectX = 350;
            textGirlNow.RectY = 0;
            textGirlNow.Source = imageOut;
            textGirlNow.FontSize = 60;
            textGirlNow.ColorText = Color.Black;
            textGirlNow.StringAlignment = StringAlignment.Far;
            textGirlNow.LineAlignment = StringAlignment.Far;
            textGirlNow.FontName = "Lato";
            //textGirlNow.debug = true;
            imageOut = textGirlNow.DrawTextWithEffects();
            // Пишем цифры
            TextGenerate textGirlOld = new TextGenerate();
            textGirlOld.TextString = ((girlNow - girlOld) > 0 ? "+" : "") + (girlNow - girlOld).ToString();
            textGirlOld.RectH = 80;
            textGirlOld.RectW = 135;
            textGirlOld.RectX = 475;
            textGirlOld.RectY = 0;
            textGirlOld.Source = imageOut;
            textGirlOld.FontSize = 44;
            textGirlOld.ColorText = Color.FromArgb(116, 116, 116);
            textGirlOld.StringAlignment = StringAlignment.Near;
            textGirlOld.LineAlignment = StringAlignment.Far;
            textGirlOld.FontName = "Lato";
            //textGirlOld.debug = true;
            imageOut = textGirlOld.DrawTextWithEffects();
            // Пишем цифры
            TextGenerate textBoyNow = new TextGenerate();
            textBoyNow.TextString = boyNow.ToString();
            textBoyNow.RectH = 85;
            textBoyNow.RectW = 130;
            textBoyNow.RectX = 350;
            textBoyNow.RectY = 70;
            textBoyNow.Source = imageOut;
            textBoyNow.FontSize = 60;
            textBoyNow.ColorText = Color.Black;
            textBoyNow.StringAlignment = StringAlignment.Far;
            textBoyNow.LineAlignment = StringAlignment.Far;
            textBoyNow.FontName = "Lato";
            //textBoyNow.debug = true;
            imageOut = textBoyNow.DrawTextWithEffects();
            // Пишем цифры
            TextGenerate textBoyOld = new TextGenerate();
            textBoyOld.TextString = ((boyNow - boyOld) > 0 ? "+" : "") + (boyNow - boyOld).ToString();
            textBoyOld.RectH = 80;
            textBoyOld.RectW = 135;
            textBoyOld.RectX = 475;
            textBoyOld.RectY = 70;
            textBoyOld.Source = imageOut;
            textBoyOld.FontSize = 44;
            textBoyOld.ColorText = Color.FromArgb(116, 116, 116);
            textBoyOld.StringAlignment = StringAlignment.Near;
            textBoyOld.LineAlignment = StringAlignment.Far;
            textBoyOld.FontName = "Lato";
            //textBoyOld.debug = true;
            imageOut = textBoyOld.DrawTextWithEffects();
            return imageOut;
        }

        public Image RenderGraphYears(int value1, int value2, int yearNow)
        {
            // Генерируем новое изображение
            PhotoGenerate photo = new PhotoGenerate();
            photo.Height = 150;
            photo.Width = 620;
            photo.ColorMain = Color.White;
            Image imageOut = photo.Fill();
            // Рисуем верхний график
            int widht = 350;
            try
            {
                widht = value1 > value2 ? 350 : (350 / value2 * value1);
            }
            catch
            {
                widht = 130;
            }

            if (widht <= 130) widht = 130;
            photo.Width = widht;

            photo.Height = 70;

            photo.ColorMain = Color.FromArgb(50, 141, 212);
            Image image1 = photo.Fill();
            // Рисуем нижний график
            Image image2 = null;


            try
            {
                widht = value1 < value2 ? 350 : (350 / value1 * value2);
            }
            catch
            {
                widht = 130;
            }

            if (widht <= 130) widht = 130;
            photo.Width = widht;
            photo.Height = 70;
            photo.ColorMain = Color.FromArgb(62, 181, 241);

            image2 = photo.Fill();

            // Объединяем слои
            Merge mergeImage = new Merge();
            mergeImage.sourceBottom = imageOut;
            mergeImage.top = 10;
            mergeImage.sourceTop = image1;
            imageOut = mergeImage.MergeImage(); // На фон помещаем размытую фотографию
            mergeImage.sourceBottom = imageOut;
            mergeImage.sourceTop = image2;
            mergeImage.top = 80;
            imageOut = mergeImage.MergeImage(); // На фон помещаем размытую фотографию

            // Пишем цифры знчениие 1
            TextGenerate textAutor = new TextGenerate();
            textAutor.TextString = value1.ToString();
            textAutor.RectH = 70;
            textAutor.RectW = 230;
            textAutor.RectX = 350;
            textAutor.RectY = 10;
            textAutor.Source = imageOut;
            textAutor.FontSize = 60;
            textAutor.ColorText = Color.Black;
            textAutor.StringAlignment = StringAlignment.Center;
            textAutor.LineAlignment = StringAlignment.Center;
            textAutor.FontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();
            // Пишем год знчениие 1
            textAutor = new TextGenerate();
            textAutor.TextString = yearNow.ToString();
            textAutor.RectH = 70;
            textAutor.RectW = 130;
            textAutor.RectX = 0;
            textAutor.RectY = 10;
            textAutor.Source = imageOut;
            textAutor.FontSize = 40;
            textAutor.ColorText = Color.White;
            textAutor.StringAlignment = StringAlignment.Center;
            textAutor.LineAlignment = StringAlignment.Center;
            textAutor.FontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();
            // Пишем цифры значение 2
            textAutor.TextString = value2.ToString();
            textAutor.RectH = 70;
            textAutor.RectW = 230;
            textAutor.RectX = 350;
            textAutor.RectY = 80;
            textAutor.Source = imageOut;
            textAutor.FontSize = 60;
            textAutor.ColorText = Color.Black;
            textAutor.StringAlignment = StringAlignment.Center;
            textAutor.LineAlignment = StringAlignment.Center;
            textAutor.FontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();
            // Пишем год знчениие 2
            textAutor = new TextGenerate();
            textAutor.TextString = (yearNow - 1).ToString();
            textAutor.RectH = 70;
            textAutor.RectW = 130;
            textAutor.RectX = 0;
            textAutor.RectY = 80;
            textAutor.Source = imageOut;
            textAutor.FontSize = 40;
            textAutor.ColorText = Color.White;
            textAutor.StringAlignment = StringAlignment.Center;
            textAutor.LineAlignment = StringAlignment.Center;
            textAutor.FontName = "Lato";
            //textAutor.debug = true;
            imageOut = textAutor.DrawTextWithEffects();

            return imageOut;
        }
    }
}
