    // Scripts/Podmladak.js
//Izracunjavanje za obraranje Brentace....
function izracunajSrednjuVrednost() {
    console.log("izracunajSrednjuVrednost pokrenut");

    var sst = document.getElementById('VremeBrentaca_SST');
    var smv = document.getElementById('VremeBrentaca_SMV');
    var gs = document.getElementById('VremeBrentaca_GS');
    var lblSrednja = document.getElementById('lblSrednjaVrednost');

    if (!sst || !smv || !gs || !lblSrednja) {
        console.log("Neki element nije pronađen");
        return;
    }

    var vremeSST = parseFloat(sst.value.replace(',', '.')) || 0;
    var vremeSMV = parseFloat(smv.value.replace(',', '.')) || 0;
    var vremeGS = parseFloat(gs.value.replace(',', '.')) || 0;

    var brojValidnih = 0;
    var suma = 0;

    if (!isNaN(vremeSST) && vremeSST > 0) { suma += vremeSST; brojValidnih++; }
    if (!isNaN(vremeSMV) && vremeSMV > 0) { suma += vremeSMV; brojValidnih++; }
    if (!isNaN(vremeGS) && vremeGS > 0) { suma += vremeGS; brojValidnih++; }

    var srednja = brojValidnih > 0 ? (suma / brojValidnih) : 0;
    lblSrednja.innerText = 'Средња вредност времена брентача: ' + srednja.toFixed(2);
    lblSrednja.className = 'text-success';

    izracunajZbirGresaka();
    izracunajUkupanPlasman();
}


function izracunajZbirGresaka() {
    console.log("izracunajZbirGresaka pokrenut");

    // Greške ×5
    var greska2_SST = document.getElementById('Greska2_Brentaca_SST');
    var greska2_SMV = document.getElementById('Greska2_Brentaca_SMV');
    var greska2_GS = document.getElementById('Greska2_Brentaca_GS');

    // Greške ×10
    var greska3_SST = document.getElementById('Greska3_Brentaca_SST');
    var greska3_SMV = document.getElementById('Greska3_Brentaca_SMV');
    var greska3_GS = document.getElementById('Greska3_Brentaca_GS');

    // Greške ×5
    var greska4_SST = document.getElementById('Greska4_Brentaca_SST');
    var greska4_SMV = document.getElementById('Greska4_Brentaca_SMV');
    var greska4_GS = document.getElementById('Greska4_Brentaca_GS');

    // Greške ×2
    var greska5_SST = document.getElementById('Greska5_Brentaca_SST');
    var greska5_SMV = document.getElementById('Greska5_Brentaca_SMV');
    var greska5_GS = document.getElementById('Greska5_Brentaca_GS');

    // Greške ×5
    var greska6_SST = document.getElementById('Greska6_Brentaca_SST');
    var greska6_SMV = document.getElementById('Greska6_Brentaca_SMV');
    var greska6_GS = document.getElementById('Greska6_Brentaca_GS');

    // Greške ×5
    var greska7_SST = document.getElementById('Greska7_Brentaca_SST');
    var greska7_SMV = document.getElementById('Greska7_Brentaca_SMV');
    var greska7_GS = document.getElementById('Greska7_Brentaca_GS');

    // Greške ×5
    var greska8_SST = document.getElementById('Greska8_Brentaca_SST');
    var greska8_SMV = document.getElementById('Greska8_Brentaca_SMV');
    var greska8_GS = document.getElementById('Greska8_Brentaca_GS');

    // Greške ×5
    var greska9_SST = document.getElementById('Greska9_Brentaca_SST');
    var greska9_SMV = document.getElementById('Greska9_Brentaca_SMV');
    var greska9_GS = document.getElementById('Greska9_Brentaca_GS');

    var lblZbir = document.getElementById('lblZbirGresaka');

    if (!greska2_SST || !greska2_SMV || !greska2_GS ||
        !greska3_SST || !greska3_SMV || !greska3_GS ||
        !greska4_SST || !greska4_SMV || !greska4_GS ||
        !greska5_SST || !greska5_SMV || !greska5_GS ||
        !greska6_SST || !greska6_SMV || !greska6_GS ||
        !greska7_SST || !greska7_SMV || !greska7_GS ||
        !greska8_SST || !greska8_SMV || !greska8_GS ||
        !greska9_SST || !greska9_SMV || !greska9_GS ||!lblZbir) return;

    // Vrednosti za greške ×5
    var vrednost2_SST = parseFloat(greska2_SST.value.replace(',', '.')) || 0;
    var vrednost2_SMV = parseFloat(greska2_SMV.value.replace(',', '.')) || 0;
    var vrednost2_GS = parseFloat(greska2_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var vrednost3_SST = parseFloat(greska3_SST.value.replace(',', '.')) || 0;
    var vrednost3_SMV = parseFloat(greska3_SMV.value.replace(',', '.')) || 0;
    var vrednost3_GS = parseFloat(greska3_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var vrednost4_SST = parseFloat(greska4_SST.value.replace(',', '.')) || 0;
    var vrednost4_SMV = parseFloat(greska4_SMV.value.replace(',', '.')) || 0;
    var vrednost4_GS = parseFloat(greska4_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×2
    var vrednost5_SST = parseFloat(greska5_SST.value.replace(',', '.')) || 0;
    var vrednost5_SMV = parseFloat(greska5_SMV.value.replace(',', '.')) || 0;
    var vrednost5_GS = parseFloat(greska5_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var vrednost6_SST = parseFloat(greska6_SST.value.replace(',', '.')) || 0;
    var vrednost6_SMV = parseFloat(greska6_SMV.value.replace(',', '.')) || 0;
    var vrednost6_GS = parseFloat(greska6_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var vrednost7_SST = parseFloat(greska7_SST.value.replace(',', '.')) || 0;
    var vrednost7_SMV = parseFloat(greska7_SMV.value.replace(',', '.')) || 0;
    var vrednost7_GS = parseFloat(greska7_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var vrednost8_SST = parseFloat(greska8_SST.value.replace(',', '.')) || 0;
    var vrednost8_SMV = parseFloat(greska8_SMV.value.replace(',', '.')) || 0;
    var vrednost8_GS = parseFloat(greska8_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var vrednost9_SST = parseFloat(greska9_SST.value.replace(',', '.')) || 0;
    var vrednost9_SMV = parseFloat(greska9_SMV.value.replace(',', '.')) || 0;
    var vrednost9_GS = parseFloat(greska9_GS.value.replace(',', '.')) || 0;

    // Izračunaj
    var zbirGresaka2 = (vrednost2_SST + vrednost2_SMV + vrednost2_GS) * 5;
    var zbirGresaka3 = (vrednost3_SST + vrednost3_SMV + vrednost3_GS) * 10;
    var zbirGresaka4 = (vrednost4_SST + vrednost4_SMV + vrednost4_GS) * 5;
    var zbirGresaka5 = (vrednost5_SST + vrednost5_SMV + vrednost5_GS) * 2;
    var zbirGresaka6 = (vrednost6_SST + vrednost6_SMV + vrednost6_GS) * 5;
    var zbirGresaka7 = (vrednost7_SST + vrednost7_SMV + vrednost7_GS) * 5;
    var zbirGresaka8 = (vrednost8_SST + vrednost8_SMV + vrednost8_GS) * 5;
    var zbirGresaka9 = (vrednost9_SST + vrednost9_SMV + vrednost9_GS) * 5;
    var ukupanZbir = zbirGresaka2 + zbirGresaka3 + zbirGresaka4 + zbirGresaka5 +
        zbirGresaka6 + zbirGresaka7 + zbirGresaka8 + zbirGresaka9;

    lblZbir.innerText = 'Збир грешака: ' + ukupanZbir.toFixed(2);
    lblZbir.className = 'text-info font-weight-bold';

    izracunajUkupanPlasman();
}
function izracunajZbirGresakaStafeta() {
    console.log("izracunajZbirGresakaStafeta pokrenut");

    // Greške ×1
    var greska1_PVSST = document.getElementById('Greska1_PV_SST');
    var greska1_PVSMV = document.getElementById('Greska1_PV_SMV');
    var greska1_PVGS = document.getElementById('Greska1_PV_GS');

    // Greške ×2
    var greska2_PVSST = document.getElementById('Greska2_PV_SST');
    var greska2_PVSMV = document.getElementById('Greska2_PV_SMV');
    var greska2_PVGS = document.getElementById('Greska2_PV_GS');

    // Greške ×2
    var greska3_PVSST = document.getElementById('Greska3_PV_SST');
    var greska3_PVSMV = document.getElementById('Greska3_PV_SMV');
    var greska3_PVGS = document.getElementById('Greska3_PV_GS');

    // Greške ×5
    var greska4_PVSST = document.getElementById('Greska4_PV_SST');
    var greska4_PVSMV = document.getElementById('Greska4_PV_SMV');
    var greska4_PVGS = document.getElementById('Greska4_PV_GS');

    // Greške ×10
    var greska5_PVSST = document.getElementById('Greska5_PV_SST');
    var greska5_PVSMV = document.getElementById('Greska5_PV_SMV');
    var greska5_PVGS = document.getElementById('Greska5_PV_GS');

    // Greške ×2
    var greska6_PVSST = document.getElementById('Greska6_PV_SST');
    var greska6_PVSMV = document.getElementById('Greska6_PV_SMV');
    var greska6_PVGS = document.getElementById('Greska6_PV_GS');

    var lblZbirGresakaStafeta = document.getElementById('lblZbirGresakaStafeta');

    if (!greska1_PVSST || !greska1_PVSMV || !greska1_PVGS ||
        !greska2_PVSST || !greska2_PVSMV || !greska2_PVGS ||
        !greska3_PVSST || !greska3_PVSMV || !greska3_PVGS ||
        !greska4_PVSST || !greska4_PVSMV || !greska4_PVGS ||
        !greska5_PVSST || !greska5_PVSMV || !greska5_PVGS ||
        !greska6_PVSST || !greska6_PVSMV || !greska6_PVGS || !lblZbirGresakaStafeta) return;

    // Vrednosti za greške ×1
    var vrednost1_PVSST = parseFloat(greska1_PVSST.value.replace(',', '.')) || 0;
    var vrednost1_PVSMV = parseFloat(greska1_PVSMV.value.replace(',', '.')) || 0;
    var vrednost1_PVGS = parseFloat(greska1_PVGS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×2
    var vrednost2_PVSST = parseFloat(greska2_PVSST.value.replace(',', '.')) || 0;
    var vrednost2_PVSMV = parseFloat(greska2_PVSMV.value.replace(',', '.')) || 0;
    var vrednost2_PVGS = parseFloat(greska2_PVGS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×2
    var vrednost3_PVSST = parseFloat(greska3_PVSST.value.replace(',', '.')) || 0;
    var vrednost3_PVSMV = parseFloat(greska3_PVSMV.value.replace(',', '.')) || 0;
    var vrednost3_PVGS = parseFloat(greska3_PVGS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var vrednost4_PVSST = parseFloat(greska4_PVSST.value.replace(',', '.')) || 0;
    var vrednost4_PVSMV = parseFloat(greska4_PVSMV.value.replace(',', '.')) || 0;
    var vrednost4_PVGS = parseFloat(greska4_PVGS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var vrednost5_PVSST = parseFloat(greska5_PVSST.value.replace(',', '.')) || 0;
    var vrednost5_PVSMV = parseFloat(greska5_PVSMV.value.replace(',', '.')) || 0;
    var vrednost5_PVGS = parseFloat(greska5_PVGS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×2
    var vrednost6_PVSST = parseFloat(greska6_PVSST.value.replace(',', '.')) || 0;
    var vrednost6_PVSMV = parseFloat(greska6_PVSMV.value.replace(',', '.')) || 0;
    var vrednost6_PVGS = parseFloat(greska6_PVGS.value.replace(',', '.')) || 0;

    
    // Izračunaj
    var zbirGresaka1 = (vrednost1_PVSST + vrednost1_PVSMV + vrednost1_PVGS) * 1;
    var zbirGresaka2 = (vrednost2_PVSST + vrednost2_PVSMV + vrednost2_PVGS) * 2;
    var zbirGresaka3 = (vrednost3_PVSST + vrednost3_PVSMV + vrednost3_PVGS) * 2;
    var zbirGresaka4 = (vrednost4_PVSST + vrednost4_PVSMV + vrednost4_PVGS) * 5;
    var zbirGresaka5 = (vrednost5_PVSST + vrednost5_PVSMV + vrednost5_PVGS) * 10;
    var zbirGresaka6 = (vrednost6_PVSST + vrednost6_PVSMV + vrednost6_PVGS) * 2;
    
    var ukupanZbirStafeta = zbirGresaka1 + zbirGresaka2 + zbirGresaka3 + zbirGresaka4 +
        zbirGresaka5 + zbirGresaka6;

    lblZbirGresakaStafeta.innerText = 'Збир грешака преношење воде: ' + ukupanZbirStafeta.toFixed(2);
    lblZbirGresakaStafeta.className = 'text-info font-weight-bold';

    izracunajUkupanPlasman();
}
function izracunajUkupanPlasman() {
    console.log("izracunajUkupanPlasman pokrenut");

    // Pročitajte početne bodove iz label-e
    var lblPocetniBodovi = document.getElementById('lblPocetniBodovi');
    var lblSrednja = document.getElementById('lblSrednjaVrednost');
    var lblZbir = document.getElementById('lblZbirGresaka');
    var lblZbirGresakaStafeta = document.getElementById('lblZbirGresakaStafeta');
    var lblUkupanPlasman = document.getElementById('lblUkupanPlasman');

    if (!lblPocetniBodovi || !lblSrednja || !lblZbir || !lblZbirGresakaStafeta || !lblUkupanPlasman) return;

    // Ekstrahuj brojčane vrednosti iz labela
    var pocetniBodoviText = lblPocetniBodovi.innerText;
    var pocetniBodovi = 0;

    // Ekstrakcija broja iz teksta "Почетни бодови: 100.00"
    if (pocetniBodoviText.includes(':')) {
        var bodoviMatch = pocetniBodoviText.match(/(\d+\.?\d*)/);
        if (bodoviMatch) {
            pocetniBodovi = parseFloat(bodoviMatch[0]);
        }
    }

    // Ekstrakcija vrednosti iz ostalih labela
    var srednjaVrednost = ekstrahujBrojIzTeksta(lblSrednja.innerText);
    var zbirGresaka = ekstrahujBrojIzTeksta(lblZbir.innerText);
    var zbirGresakaStafeta = ekstrahujBrojIzTeksta(lblZbirGresakaStafeta.innerText);

    // Izračunaj ukupan rezultat
    var ukupanPlasman = pocetniBodovi - (srednjaVrednost + zbirGresaka + zbirGresakaStafeta);

    // Prikaži rezultat (zaokruži na 2 decimale)
    lblUkupanPlasman.innerText = 'Укупан резултат: ' + ukupanPlasman.toFixed(2);

    // Promeni boju na osnovu rezultata
    if (ukupanPlasman < 0) {
        lblUkupanPlasman.className = 'text-danger font-weight-bold ml-3';
    } else {
        lblUkupanPlasman.className = 'text-success font-weight-bold ml-3';
    }
}

// Pomocna funkcija za ekstrakciju broja iz teksta
function ekstrahujBrojIzTeksta(text) {
    var match = text.match(/(\d+\.?\d*)/);
    return match ? parseFloat(match[0]) : 0;
}
function initPodmladak() {
    console.log("initPodmladak pokrenut");

    // Za vremena
    var sst = document.getElementById('VremeBrentaca_SST');
    var smv = document.getElementById('VremeBrentaca_SMV');
    var gs = document.getElementById('VremeBrentaca_GS');

    if (sst) sst.addEventListener('input', izracunajSrednjuVrednost);
    if (smv) smv.addEventListener('input', izracunajSrednjuVrednost);
    if (gs) gs.addEventListener('input', izracunajSrednjuVrednost);

    // Za greške ×5
    var greska2_SST = document.getElementById('Greska2_Brentaca_SST');
    var greska2_SMV = document.getElementById('Greska2_Brentaca_SMV');
    var greska2_GS = document.getElementById('Greska2_Brentaca_GS');

    if (greska2_SST) greska2_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska2_SMV) greska2_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska2_GS) greska2_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×10
    var greska3_SST = document.getElementById('Greska3_Brentaca_SST');
    var greska3_SMV = document.getElementById('Greska3_Brentaca_SMV');
    var greska3_GS = document.getElementById('Greska3_Brentaca_GS');

    if (greska3_SST) greska3_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska3_SMV) greska3_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska3_GS) greska3_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×5
    var greska4_SST = document.getElementById('Greska4_Brentaca_SST');
    var greska4_SMV = document.getElementById('Greska4_Brentaca_SMV');
    var greska4_GS = document.getElementById('Greska4_Brentaca_GS');

    if (greska4_SST) greska4_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska4_SMV) greska4_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska4_GS) greska4_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×2
    var greska5_SST = document.getElementById('Greska5_Brentaca_SST');
    var greska5_SMV = document.getElementById('Greska5_Brentaca_SMV');
    var greska5_GS = document.getElementById('Greska5_Brentaca_GS');

    if (greska5_SST) greska5_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska5_SMV) greska5_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska5_GS) greska5_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×5
    var greska6_SST = document.getElementById('Greska6_Brentaca_SST');
    var greska6_SMV = document.getElementById('Greska6_Brentaca_SMV');
    var greska6_GS = document.getElementById('Greska6_Brentaca_GS');

    if (greska6_SST) greska6_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska6_SMV) greska6_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska6_GS) greska6_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×5
    var greska7_SST = document.getElementById('Greska7_Brentaca_SST');
    var greska7_SMV = document.getElementById('Greska7_Brentaca_SMV');
    var greska7_GS = document.getElementById('Greska7_Brentaca_GS');

    if (greska7_SST) greska7_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska7_SMV) greska7_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska7_GS) greska7_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×5
    var greska8_SST = document.getElementById('Greska8_Brentaca_SST');
    var greska8_SMV = document.getElementById('Greska8_Brentaca_SMV');
    var greska8_GS = document.getElementById('Greska8_Brentaca_GS');

    if (greska8_SST) greska8_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska8_SMV) greska8_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska8_GS) greska8_GS.addEventListener('input', izracunajZbirGresaka);

    // Za greške ×5
    var greska9_SST = document.getElementById('Greska9_Brentaca_SST');
    var greska9_SMV = document.getElementById('Greska9_Brentaca_SMV');
    var greska9_GS = document.getElementById('Greska9_Brentaca_GS');

    if (greska9_SST) greska9_SST.addEventListener('input', izracunajZbirGresaka);
    if (greska9_SMV) greska9_SMV.addEventListener('input', izracunajZbirGresaka);
    if (greska9_GS) greska9_GS.addEventListener('input', izracunajZbirGresaka);

    // Greške ×1
    var greska1_PVSST = document.getElementById('Greska1_PV_SST');
    var greska1_PVSMV = document.getElementById('Greska1_PV_SMV');
    var greska1_PVGS = document.getElementById('Greska1_PV_GS');

    if (greska1_PVSST) greska1_PVSST.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska1_PVSMV) greska1_PVSMV.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska1_PVGS) greska1_PVGS.addEventListener('input', izracunajZbirGresakaStafeta);

    // Greške ×2
    var greska2_PVSST = document.getElementById('Greska2_PV_SST');
    var greska2_PVSMV = document.getElementById('Greska2_PV_SMV');
    var greska2_PVGS = document.getElementById('Greska2_PV_GS');

    if (greska2_PVSST) greska2_PVSST.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska2_PVSMV) greska2_PVSMV.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska2_PVGS) greska2_PVGS.addEventListener('input', izracunajZbirGresakaStafeta);

    // Greške ×2
    var greska3_PVSST = document.getElementById('Greska3_PV_SST');
    var greska3_PVSMV = document.getElementById('Greska3_PV_SMV');
    var greska3_PVGS = document.getElementById('Greska3_PV_GS');

    if (greska3_PVSST) greska3_PVSST.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska3_PVSMV) greska3_PVSMV.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska3_PVGS) greska3_PVGS.addEventListener('input', izracunajZbirGresakaStafeta);

    // Greške ×5
    var greska4_PVSST = document.getElementById('Greska4_PV_SST');
    var greska4_PVSMV = document.getElementById('Greska4_PV_SMV');
    var greska4_PVGS = document.getElementById('Greska4_PV_GS');

    if (greska4_PVSST) greska4_PVSST.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska4_PVSMV) greska4_PVSMV.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska4_PVGS) greska4_PVGS.addEventListener('input', izracunajZbirGresakaStafeta);

    // Greške ×10
    var greska5_PVSST = document.getElementById('Greska5_PV_SST');
    var greska5_PVSMV = document.getElementById('Greska5_PV_SMV');
    var greska5_PVGS = document.getElementById('Greska5_PV_GS');

    if (greska5_PVSST) greska5_PVSST.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska5_PVSMV) greska5_PVSMV.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska5_PVGS) greska5_PVGS.addEventListener('input', izracunajZbirGresakaStafeta);

    // Greške ×2
    var greska6_PVSST = document.getElementById('Greska6_PV_SST');
    var greska6_PVSMV = document.getElementById('Greska6_PV_SMV');
    var greska6_PVGS = document.getElementById('Greska6_PV_GS');

    if (greska6_PVSST) greska6_PVSST.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska6_PVSMV) greska6_PVSMV.addEventListener('input', izracunajZbirGresakaStafeta);
    if (greska6_PVGS) greska6_PVGS.addEventListener('input', izracunajZbirGresakaStafeta);

    var allInputs = document.querySelectorAll('input[type="text"]');
    allInputs.forEach(function (input) {
        input.addEventListener('input', function () {
            izracunajSrednjuVrednost();
            izracunajZbirGresaka();
            izracunajZbirGresakaStafeta();
            izracunajUkupanPlasman();
        });
    });

    // Inicijalno izračunavanje
    izracunajSrednjuVrednost();
    izracunajZbirGresaka();
    izracunajZbirGresakaStafeta();
    izracunajUkupanPlasman();
}
// Pokreni kada se stranica učita
document.addEventListener('DOMContentLoaded', initPodmladak);