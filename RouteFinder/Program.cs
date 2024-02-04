using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Graph graph = new Graph();

        City kyiv = new City("Kyiv");
        City lviv = new City("Lviv");
        City kharkiv = new City("Kharkiv");
        City odesa = new City("Odesa");
        City dnipro = new City("Dnipro");
        City zaporizhzhia = new City("Zaporizhzhia");
        City donetsk = new City("Donetsk");
        City luhansk = new City("Luhansk");
        City kherson = new City("Kherson");
        City sumy = new City("Sumy");
        City chernivtsi = new City("Chernivtsi");
        City ivanoFrankivsk = new City("Ivano-Frankivsk");
        City ternopil = new City("Ternopil");
        City uzhhorod = new City("Uzhhorod");
        City vinnytsia = new City("Vinnytsia");
        City zhytomyr = new City("Zhytomyr");
        City rivne = new City("Rivne");
        City cherkasy = new City("Cherkasy");
        City kropyvnytskyi = new City("Kropyvnytskyi");
        City mykolaiv = new City("Mykolaiv");

        graph.AddCity(kyiv);
        graph.AddCity(lviv);
        graph.AddCity(kharkiv);
        graph.AddCity(odesa);
        graph.AddCity(dnipro);
        graph.AddCity(zaporizhzhia);
        graph.AddCity(donetsk);
        graph.AddCity(luhansk);
        graph.AddCity(kherson);
        graph.AddCity(sumy);
        graph.AddCity(chernivtsi);
        graph.AddCity(ivanoFrankivsk);
        graph.AddCity(ternopil);
        graph.AddCity(uzhhorod);
        graph.AddCity(vinnytsia);
        graph.AddCity(zhytomyr);
        graph.AddCity(rivne);
        graph.AddCity(cherkasy);
        graph.AddCity(kropyvnytskyi);
        graph.AddCity(mykolaiv);

        graph.AddDistance(kyiv, lviv, 540);
        graph.AddDistance(kyiv, kharkiv, 480);
        graph.AddDistance(kyiv, odesa, 470);
        graph.AddDistance(kyiv, dnipro, 340);
        graph.AddDistance(kyiv, zaporizhzhia, 470);
        graph.AddDistance(kyiv, donetsk, 620);
        graph.AddDistance(kyiv, luhansk, 700);
        graph.AddDistance(kyiv, kherson, 470);
        graph.AddDistance(kyiv, sumy, 330);
        graph.AddDistance(kyiv, chernivtsi, 580);
        graph.AddDistance(kyiv, ivanoFrankivsk, 560);
        graph.AddDistance(kyiv, ternopil, 520);
        graph.AddDistance(kyiv, uzhhorod, 780);
        graph.AddDistance(kyiv, vinnytsia, 270);
        graph.AddDistance(kyiv, zhytomyr, 140);
        graph.AddDistance(kyiv, rivne, 280);
        graph.AddDistance(kyiv, cherkasy, 190);
        graph.AddDistance(kyiv, kropyvnytskyi, 310);
        graph.AddDistance(kyiv, mykolaiv, 480);

        graph.AddDistance(lviv, kharkiv, 720);
        graph.AddDistance(lviv, odesa, 860);
        graph.AddDistance(lviv, dnipro, 780);
        graph.AddDistance(lviv, zaporizhzhia, 1009);
        graph.AddDistance(lviv, donetsk, 1215);
        graph.AddDistance(lviv, luhansk, 1377);
        graph.AddDistance(lviv, kherson, 935);
        graph.AddDistance(lviv, sumy, 873);
        graph.AddDistance(lviv, chernivtsi, 140);
        graph.AddDistance(lviv, ivanoFrankivsk, 80);
        graph.AddDistance(lviv, ternopil, 110);
        graph.AddDistance(lviv, uzhhorod, 270);
        graph.AddDistance(lviv, vinnytsia, 310);
        graph.AddDistance(lviv, zhytomyr, 450);
        graph.AddDistance(lviv, rivne, 330);
        graph.AddDistance(lviv, cherkasy, 390);
        graph.AddDistance(lviv, kropyvnytskyi, 500);
        graph.AddDistance(lviv, mykolaiv, 874);

        graph.AddDistance(kharkiv, odesa, 610);
        graph.AddDistance(kharkiv, dnipro, 380);
        graph.AddDistance(kharkiv, zaporizhzhia, 298);
        graph.AddDistance(kharkiv, donetsk, 335);
        graph.AddDistance(kharkiv, luhansk, 339);
        graph.AddDistance(kharkiv, kherson, 628);
        graph.AddDistance(kharkiv, sumy, 280);
        graph.AddDistance(kharkiv, chernivtsi, 1025);
        graph.AddDistance(kharkiv, ivanoFrankivsk, 1037);
        graph.AddDistance(kharkiv, ternopil, 908);
        graph.AddDistance(kharkiv, uzhhorod, 1288);
        graph.AddDistance(kharkiv, vinnytsia, 712);
        graph.AddDistance(kharkiv, zhytomyr, 619);
        graph.AddDistance(kharkiv, rivne, 807);
        graph.AddDistance(kharkiv, cherkasy, 397);
        graph.AddDistance(kharkiv, kropyvnytskyi, 385);
        graph.AddDistance(kharkiv, mykolaiv, 567);

        graph.AddDistance(odesa, dnipro, 560);
        graph.AddDistance(odesa, zaporizhzhia, 370);
        graph.AddDistance(odesa, donetsk, 568);
        graph.AddDistance(odesa, luhansk, 700);
        graph.AddDistance(odesa, kherson, 200);
        graph.AddDistance(odesa, sumy, 570);
        graph.AddDistance(odesa, chernivtsi, 410);
        graph.AddDistance(odesa, ivanoFrankivsk, 550);
        graph.AddDistance(odesa, ternopil, 505);
        graph.AddDistance(odesa, uzhhorod, 700);
        graph.AddDistance(odesa, vinnytsia, 353);
        graph.AddDistance(odesa, zhytomyr, 446);
        graph.AddDistance(odesa, rivne, 560);
        graph.AddDistance(odesa, cherkasy, 345);
        graph.AddDistance(odesa, kropyvnytskyi, 300);
        graph.AddDistance(odesa, mykolaiv, 330);

        graph.AddDistance(dnipro, zaporizhzhia, 170);
        graph.AddDistance(dnipro, donetsk, 210);
        graph.AddDistance(dnipro, luhansk, 315);
        graph.AddDistance(dnipro, kherson, 271);
        graph.AddDistance(dnipro, sumy, 273);
        graph.AddDistance(dnipro, chernivtsi, 774);
        graph.AddDistance(dnipro, ivanoFrankivsk, 770);
        graph.AddDistance(dnipro, ternopil, 707);
        graph.AddDistance(dnipro, uzhhorod, 1100);
        graph.AddDistance(dnipro, vinnytsia, 512);
        graph.AddDistance(dnipro, zhytomyr, 510);
        graph.AddDistance(dnipro, rivne, 665);
        graph.AddDistance(dnipro, cherkasy, 246);
        graph.AddDistance(dnipro, kropyvnytskyi, 310);
        graph.AddDistance(dnipro, mykolaiv, 280);

        graph.AddDistance(zaporizhzhia, donetsk, 520);
        graph.AddDistance(zaporizhzhia, luhansk, 321);
        graph.AddDistance(zaporizhzhia, kherson, 280);
        graph.AddDistance(zaporizhzhia, sumy, 343);
        graph.AddDistance(zaporizhzhia, chernivtsi, 683);
        graph.AddDistance(zaporizhzhia, ivanoFrankivsk, 771);
        graph.AddDistance(zaporizhzhia, ternopil, 715);
        graph.AddDistance(zaporizhzhia, uzhhorod, 948);
        graph.AddDistance(zaporizhzhia, vinnytsia, 521);
        graph.AddDistance(zaporizhzhia, zhytomyr, 534);
        graph.AddDistance(zaporizhzhia, rivne, 732);
        graph.AddDistance(zaporizhzhia, cherkasy, 286);
        graph.AddDistance(zaporizhzhia, kropyvnytskyi, 227);
        graph.AddDistance(zaporizhzhia, mykolaiv, 255);

        graph.AddDistance(donetsk, luhansk, 380);
        graph.AddDistance(donetsk, kherson, 127);
        graph.AddDistance(donetsk, sumy, 385);
        graph.AddDistance(donetsk, chernivtsi, 879);
        graph.AddDistance(donetsk, ivanoFrankivsk, 979);
        graph.AddDistance(donetsk, ternopil, 897);
        graph.AddDistance(donetsk, uzhhorod, 1142);
        graph.AddDistance(donetsk, vinnytsia, 691);
        graph.AddDistance(donetsk, zhytomyr, 725);
        graph.AddDistance(donetsk, rivne, 865);
        graph.AddDistance(donetsk, cherkasy, 445);
        graph.AddDistance(donetsk, kropyvnytskyi, 416);
        graph.AddDistance(donetsk, mykolaiv, 457);

        graph.AddDistance(luhansk, kherson, 538);
        graph.AddDistance(luhansk, sumy, 421);
        graph.AddDistance(luhansk, chernivtsi, 990);
        graph.AddDistance(luhansk, ivanoFrankivsk, 1067);
        graph.AddDistance(luhansk, ternopil, 1015);
        graph.AddDistance(luhansk, uzhhorod, 1252);
        graph.AddDistance(luhansk, vinnytsia, 800);
        graph.AddDistance(luhansk, zhytomyr, 780);
        graph.AddDistance(luhansk, rivne, 949);
        graph.AddDistance(luhansk, cherkasy, 542);
        graph.AddDistance(luhansk, kropyvnytskyi, 518);
        graph.AddDistance(luhansk, mykolaiv, 1111);

        graph.AddDistance(kherson, sumy, 499);
        graph.AddDistance(kherson, chernivtsi, 527);
        graph.AddDistance(kherson, ivanoFrankivsk, 629);
        graph.AddDistance(kherson, ternopil, 601);
        graph.AddDistance(kherson, uzhhorod, 789);
        graph.AddDistance(kherson, vinnytsia, 416);
        graph.AddDistance(kherson, zhytomyr, 460);
        graph.AddDistance(kherson, rivne, 630);
        graph.AddDistance(kherson, cherkasy, 314);
        graph.AddDistance(kherson, kropyvnytskyi, 209);
        graph.AddDistance(kherson, mykolaiv, 60);

        graph.AddDistance(sumy, chernivtsi, 687);
        graph.AddDistance(sumy, ivanoFrankivsk, 770);
        graph.AddDistance(sumy, ternopil, 681);
        graph.AddDistance(sumy, uzhhorod, 954);
        graph.AddDistance(sumy, vinnytsia, 496);
        graph.AddDistance(sumy, zhytomyr, 443);
        graph.AddDistance(sumy, rivne, 601);
        graph.AddDistance(sumy, cherkasy, 257);
        graph.AddDistance(sumy, kropyvnytskyi, 321);
        graph.AddDistance(sumy, mykolaiv, 488);

        graph.AddDistance(chernivtsi, ivanoFrankivsk, 113);
        graph.AddDistance(chernivtsi, ternopil, 142);
        graph.AddDistance(chernivtsi, uzhhorod, 272);
        graph.AddDistance(chernivtsi, vinnytsia, 215);
        graph.AddDistance(chernivtsi, zhytomyr, 292);
        graph.AddDistance(chernivtsi, rivne, 260);
        graph.AddDistance(chernivtsi, cherkasy, 471);
        graph.AddDistance(chernivtsi, kropyvnytskyi, 469);
        graph.AddDistance(chernivtsi, mykolaiv, 471);

        graph.AddDistance(ivanoFrankivsk, ternopil, 97);
        graph.AddDistance(ivanoFrankivsk, uzhhorod, 181);
        graph.AddDistance(ivanoFrankivsk, vinnytsia, 277);
        graph.AddDistance(ivanoFrankivsk, zhytomyr, 324);
        graph.AddDistance(ivanoFrankivsk, rivne, 220);
        graph.AddDistance(ivanoFrankivsk, cherkasy, 540);
        graph.AddDistance(ivanoFrankivsk, kropyvnytskyi, 558);
        graph.AddDistance(ivanoFrankivsk, mykolaiv, 574);

        graph.AddDistance(ternopil, uzhhorod, 260);
        graph.AddDistance(ternopil, vinnytsia, 211);
        graph.AddDistance(ternopil, zhytomyr, 234);
        graph.AddDistance(ternopil, rivne, 128);
        graph.AddDistance(ternopil, cherkasy, 466);
        graph.AddDistance(ternopil, kropyvnytskyi, 495);
        graph.AddDistance(ternopil, mykolaiv, 544);

        graph.AddDistance(uzhhorod, vinnytsia, 459);
        graph.AddDistance(uzhhorod, zhytomyr, 502);
        graph.AddDistance(uzhhorod, rivne, 357);
        graph.AddDistance(uzhhorod, cherkasy, 724);
        graph.AddDistance(uzhhorod, kropyvnytskyi, 733);
        graph.AddDistance(uzhhorod, mykolaiv, 758);

        graph.AddDistance(vinnytsia, zhytomyr, 114);
        graph.AddDistance(vinnytsia, rivne, 223);
        graph.AddDistance(vinnytsia, cherkasy, 261);
        graph.AddDistance(vinnytsia, kropyvnytskyi, 287);
        graph.AddDistance(vinnytsia, mykolaiv, 359);

        graph.AddDistance(zhytomyr, rivne, 176);
        graph.AddDistance(zhytomyr, cherkasy, 258);
        graph.AddDistance(zhytomyr, kropyvnytskyi, 322);
        graph.AddDistance(zhytomyr, mykolaiv, 436);

        graph.AddDistance(rivne, cherkasy, 430);
        graph.AddDistance(rivne, kropyvnytskyi, 485);
        graph.AddDistance(rivne, mykolaiv, 574);

        graph.AddDistance(kropyvnytskyi, cherkasy, 105);
        graph.AddDistance(kropyvnytskyi, mykolaiv, 173);

        graph.AddDistance(cherkasy, mykolaiv, 276);

        City startCity = luhansk;
        City endCity = uzhhorod;

        List<City> shortestPath = graph.FindShortestPath(startCity, endCity);

        Console.WriteLine($"Shortest path from {startCity.Name} to {endCity.Name}: {string.Join(" -> ", shortestPath.Select(c => c.Name))}");
    }
}