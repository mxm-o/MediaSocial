using System;
using System.Text.RegularExpressions;

namespace PluginKirsanovWeatherManual
{
    public class Weather
    {
        public HtmlAgilityPack.HtmlDocument loadHtml(bool cache, out bool result)
        {
            DateTime dateTimeCache = Global.siteTime;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            DateTime dateTimeNow = DateTime.Now;
            result = false;

            if (cache && (dateTimeCache.AddHours(5) > dateTimeNow))
            {
                doc.LoadHtml(Global.siteHtml);
            }
            else
            {
                try
                {
                    //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    var webGet = new HtmlAgilityPack.HtmlWeb();
                    //doc.LoadHtml(s);
                    doc = webGet.Load(Global.sitePath);
                    DateTime dateTimeMax = dateTimeNow;
                    Global.siteTime = dateTimeMax;
                    Global.siteHtml = doc.Text;
                    Configuration.Save();
                    result = false;
                }
                catch
                {
                    result = true;
                }
            }
            return doc;
        }

        public bool parsePogoda()
        {
            bool errorLoad;

            var doc = loadHtml(true, out errorLoad);

            if (errorLoad)
            {
                return false;
            }

            for (int j = 0; j < Global.weatherSetting.Count; j++)
            {
                try
                {
                    string end = "";
                    HtmlAgilityPack.HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode(Global.weatherSetting[j].XPath.Replace("x:", ""));
                    if (Global.weatherSetting[j].XPathType == "innerText") end = bodyNode.InnerText;
                    if (Global.weatherSetting[j].XPathType == "innerHtml") end = bodyNode.InnerHtml;
                    if (Global.weatherSetting[j].XPathType == "title") end = bodyNode.Attributes["title"].Value;
                    if (Global.weatherSetting[j].XPathType == "class") end = bodyNode.Attributes["class"].Value;
                    if (Global.weatherSetting[j].XPathType == "value") end = bodyNode.Attributes["value"].Value;
                    end = end.Replace("\t", "");
                    end = end.Replace("\n", "");

                    if (Global.weatherSetting[j].AutoClear)
                    {
                        switch (Global.weatherSetting[j].Type)
                        {
                            case "temp":
                                end = end.Replace(" &deg;C", "");
                                end = end.Replace("&deg;", "");
                                break;
                            case "cloudlong":

                                break;
                            case "cloudshort":
                                end = end.ToLower().Contains("снег") ? "Снег" : end;
                                end = end.ToLower().Contains("дождь") ? "Дождь" : end;
                                end = end.ToLower().Contains("осадки") ? "Дождь" : end;
                                end = end.ToLower().Contains("пасмурно") ? "Облачно" : end;
                                end = WeatherType.CheckWeatherClowd(end);
                                break;
                            case "winddirectionshort":
                                end = end.Replace("Ветер: ", "");
                                end = end.Replace("Ветер", "");
                                end = end.Replace("ветер", "");
                                end = end.Replace("ветер", "");
                                end = WeatherType.FilterWindDirection(end, "short");
                                break;
                            case "winddirectionlong":
                                end = end.Replace("Ветер: ", "");
                                end = end.Replace("Ветер", "");
                                end = end.Replace("ветер", "");
                                end = end.Replace("ветер", "");
                                end = WeatherType.FilterWindDirection(end, "full");
                                break;
                            case "windpower":
                                end = end.Replace("Ветер: ", "");
                                end = end.Replace("Ветер", "");
                                end = end.Replace("ветер", "");
                                end = end.Replace("ветер", "");
                                break;
                            case "pressure":
                                end = end.Replace(" мм", "");
                                end = end.Replace(" рт.ст.", "");
                                break;
                            case "humidity":
                                end = end.Replace("Влажность: ", "");
                                end = end.Replace("Влажность", "");
                                end = end.Replace("влажность", "");
                                end = end.Replace("%", "");
                                break;
                            default:

                                break;
                        }
                    }



                    if (Global.weatherSetting[j].Clear != null && Global.weatherSetting[j].Clear != "")
                    {
                        Regex regex = new Regex(@Global.weatherSetting[j].Clear);
                        Match match = regex.Match(end);
                        if (match.Success)
                        {
                            end = (match.Value).Trim();
                        }

                    }
                    Global.weatherSetting[j].Value = end;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
