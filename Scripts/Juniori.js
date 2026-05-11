// Scripts/Juniori.js
// Izracunavanje za Juniore....

function izracunajSrednjuVrednostJunioriPrepreke() {
    console.log("izracunajSrednjuVrednostJunioriPrepreke pokrenut");

    var vps1 = document.getElementById('VremePrepreke_Sudija1');
    var vps2 = document.getElementById('VremePrepreke_Sudija2');
    var vpgs = document.getElementById('VremePrepreke_GlavniSudija');
    var lblSrednjaJunioriPrepreke = document.getElementById('lblSrednjaVrednostJunioriPrepreke');

    if (!vps1 || !vps2 || !vpgs || !lblSrednjaJunioriPrepreke) {
        console.log("Neki element nije pronađen");
        return;
    }

    var VPS1 = parseFloat(vps1.value.replace(',', '.')) || 0;
    var VPS2 = parseFloat(vps2.value.replace(',', '.')) || 0;
    var VPGS = parseFloat(vpgs.value.replace(',', '.')) || 0;

    var brojValidnih = 0;
    var suma = 0;

    if (!isNaN(VPS1) && VPS1 > 0) { suma += VPS1; brojValidnih++; }
    if (!isNaN(VPS2) && VPS2 > 0) { suma += VPS2; brojValidnih++; }
    if (!isNaN(VPGS) && VPGS > 0) { suma += VPGS; brojValidnih++; }

    var srednja = brojValidnih > 0 ? (suma / brojValidnih) : 0;
    lblSrednjaJunioriPrepreke.innerText = 'Средња време препреке: ' + srednja.toFixed(2);
    lblSrednjaJunioriPrepreke.className = 'text-success';

    izracunajUkupanPlasmanJuniori();
}

function izracunajSrednjuVrednostJunioriStafeta() {
    console.log("=== izracunajSrednjuVrednostJunioriStafeta ===");

    // Probaćemo različite kombinacije ID-ova
    var vsm1 = document.getElementById('VremeStafete_Merilac1');
    var vsm2 = document.getElementById('VremeStafete_Merilac2');
    var vsgs = document.getElementById('VremeStafete_GlavniSudija');
    var lblSrednja = document.getElementById('lblSrednjaVrednostJunioriStafeta');

    // Ako nisu pronađeni, probajmo druge varijante
    if (!vsm1) vsm1 = document.querySelector('input[id*="Stafete"][id*="Merilac1"]');
    if (!vsm2) vsm2 = document.querySelector('input[id*="Stafete"][id*="Merilac2"]');
    if (!vsgs) vsgs = document.querySelector('input[id*="Stafete"][id*="GlavniSudija"]');
    if (!lblSrednja) lblSrednja = document.querySelector('span[id*="Stafeta"], label[id*="Stafeta"]');

    console.log("Pronađeni elementi:", {
        merilac1: vsm1 ? vsm1.id : "N/A",
        merilac2: vsm2 ? vsm2.id : "N/A",
        glavniSudija: vsgs ? vsgs.id : "N/A",
        labela: lblSrednja ? lblSrednja.id : "N/A"
    });

    if (!vsm1 || !vsm2 || !vsgs || !lblSrednja) {
        console.log("❌ Elementi za štafetu nisu pronađeni");

        // Prikaži sve dostupne ID-ove
        var sviInputs = document.querySelectorAll('input');
        console.log("Svi input elementi:");
        sviInputs.forEach(function (input) {
            if (input.id.includes('Stafete') || input.id.includes('Stafeta')) {
                console.log("ID:", input.id, "Vrednost:", input.value);
            }
        });

        var sveLabel = document.querySelectorAll('label, span');
        console.log("Sve label/elementi:");
        sveLabel.forEach(function (el) {
            if (el.id.includes('Stafeta') || el.innerText.includes('штафете') || el.innerText.includes('Stafeta')) {
                console.log("ID:", el.id, "Text:", el.innerText);
            }
        });

        return;
    }

    // Ako su elementi pronađeni, nastavi sa računanjem
    var VSM1 = parseFloat(vsm1.value.replace(',', '.')) || 0;
    var VSM2 = parseFloat(vsm2.value.replace(',', '.')) || 0;
    var VSGS = parseFloat(vsgs.value.replace(',', '.')) || 0;

    console.log("📊 Vrednosti štafete:", VSM1, VSM2, VSGS);

    var brojValidnih = 0;
    var suma = 0;

    if (!isNaN(VSM1) && VSM1 > 0) { suma += VSM1; brojValidnih++; }
    if (!isNaN(VSM2) && VSM2 > 0) { suma += VSM2; brojValidnih++; }
    if (!isNaN(VSGS) && VSGS > 0) { suma += VSGS; brojValidnih++; }

    var srednja = brojValidnih > 0 ? (suma / brojValidnih) : 0;

    console.log("✅ Rezultat - Broj validnih:", brojValidnih, "Suma:", suma, "Srednja:", srednja);

    lblSrednja.innerText = 'Средња време штафете: ' + srednja.toFixed(2);
    lblSrednja.className = 'text-success';

    console.log("✅ Štafeta ažurirana");
    izracunajUkupanPlasmanJuniori();
}

function izracunajZbirGresakaJunioriPrepreke() {
    console.log("izracunajZbirGresakaJunioriPrepreke pokrenut");

    // Greške ×10
    var greska2_S1 = document.getElementById('Greska2_Prep_Sudija1');
    var greska2_S2 = document.getElementById('Greska2_Prep_Sudija2');
    var greska2_S3 = document.getElementById('Greska2_Prep_Sudija3');
    var greska2_S4 = document.getElementById('Greska2_Prep_Sudija4');
    var greska2_S5 = document.getElementById('Greska2_Prep_Sudija5');
    var greska2_GS = document.getElementById('Greska2_Prep_GlavniSudija');

    // Greške ×5
    var greska3_S1 = document.getElementById('Greska3_Prep_Sudija1');
    var greska3_S2 = document.getElementById('Greska3_Prep_Sudija2');
    var greska3_S3 = document.getElementById('Greska3_Prep_Sudija3');
    var greska3_S4 = document.getElementById('Greska3_Prep_Sudija4');
    var greska3_S5 = document.getElementById('Greska3_Prep_Sudija5');
    var greska3_GS = document.getElementById('Greska3_Prep_GlavniSudija');

    // Greške ×20
    var greska4_S1 = document.getElementById('Greska4_Prep_Sudija1');
    var greska4_S2 = document.getElementById('Greska4_Prep_Sudija2');
    var greska4_S3 = document.getElementById('Greska4_Prep_Sudija3');
    var greska4_S4 = document.getElementById('Greska4_Prep_Sudija4');
    var greska4_S5 = document.getElementById('Greska4_Prep_Sudija5');
    var greska4_GS = document.getElementById('Greska4_Prep_GlavniSudija');

    // Greške ×10
    var greska5_S1 = document.getElementById('Greska5_Prep_Sudija1');
    var greska5_S2 = document.getElementById('Greska5_Prep_Sudija2');
    var greska5_S3 = document.getElementById('Greska5_Prep_Sudija3');
    var greska5_S4 = document.getElementById('Greska5_Prep_Sudija4');
    var greska5_S5 = document.getElementById('Greska5_Prep_Sudija5');
    var greska5_GS = document.getElementById('Greska5_Prep_GlavniSudija');

    // Greške ×5
    var greska6_S1 = document.getElementById('Greska6_Prep_Sudija1');
    var greska6_S2 = document.getElementById('Greska6_Prep_Sudija2');
    var greska6_S3 = document.getElementById('Greska6_Prep_Sudija3');
    var greska6_S4 = document.getElementById('Greska6_Prep_Sudija4');
    var greska6_S5 = document.getElementById('Greska6_Prep_Sudija5');
    var greska6_GS = document.getElementById('Greska6_Prep_GlavniSudija');

    // Greške ×10
    var greska7_S1 = document.getElementById('Greska7_Prep_Sudija1');
    var greska7_S2 = document.getElementById('Greska7_Prep_Sudija2');
    var greska7_S3 = document.getElementById('Greska7_Prep_Sudija3');
    var greska7_S4 = document.getElementById('Greska7_Prep_Sudija4');
    var greska7_S5 = document.getElementById('Greska7_Prep_Sudija5');
    var greska7_GS = document.getElementById('Greska7_Prep_GlavniSudija');

    // Greške x10
    var greska8_S1 = document.getElementById('Greska8_Prep_Sudija1');
    var greska8_S2 = document.getElementById('Greska8_Prep_Sudija2');
    var greska8_S3 = document.getElementById('Greska8_Prep_Sudija3');
    var greska8_S4 = document.getElementById('Greska8_Prep_Sudija4');
    var greska8_S5 = document.getElementById('Greska8_Prep_Sudija5');
    var greska8_GS = document.getElementById('Greska8_Prep_GlavniSudija');

    // Greške ×10
    var greska9_S1 = document.getElementById('Greska9_Prep_Sudija1');
    var greska9_S2 = document.getElementById('Greska9_Prep_Sudija2');
    var greska9_S3 = document.getElementById('Greska9_Prep_Sudija3');
    var greska9_S4 = document.getElementById('Greska9_Prep_Sudija4');
    var greska9_S5 = document.getElementById('Greska9_Prep_Sudija5');
    var greska9_GS = document.getElementById('Greska9_Prep_GlavniSudija');

    // Greške ×10
    var greska10_S1 = document.getElementById('Greska10_Prep_Sudija1');
    var greska10_S2 = document.getElementById('Greska10_Prep_Sudija2');
    var greska10_S3 = document.getElementById('Greska10_Prep_Sudija3');
    var greska10_S4 = document.getElementById('Greska10_Prep_Sudija4');
    var greska10_S5 = document.getElementById('Greska10_Prep_Sudija5');
    var greska10_GS = document.getElementById('Greska10_Prep_GlavniSudija');

    // Greške ×10
    var greska11_S1 = document.getElementById('Greska11_Prep_Sudija1');
    var greska11_S2 = document.getElementById('Greska11_Prep_Sudija2');
    var greska11_S3 = document.getElementById('Greska11_Prep_Sudija3');
    var greska11_S4 = document.getElementById('Greska11_Prep_Sudija4');
    var greska11_S5 = document.getElementById('Greska11_Prep_Sudija5');
    var greska11_GS = document.getElementById('Greska11_Prep_GlavniSudija');

    var lblZbirJunioriPrepreke = document.getElementById('lblZbirGresakaJunioriPrepreke');

    if (!greska2_S1 || !greska2_S2 || !greska2_S3 || !greska2_S4 || !greska2_S5 || !greska2_GS ||
        !greska3_S1 || !greska3_S2 || !greska3_S3 || !greska3_S4 || !greska3_S5 || !greska3_GS ||
        !greska4_S1 || !greska4_S2 || !greska4_S3 || !greska4_S4 || !greska4_S5 || !greska4_GS ||
        !greska5_S1 || !greska5_S2 || !greska5_S3 || !greska5_S4 || !greska5_S5 || !greska5_GS ||
        !greska6_S1 || !greska6_S2 || !greska6_S3 || !greska6_S4 || !greska6_S5 || !greska6_GS ||
        !greska7_S1 || !greska7_S2 || !greska7_S3 || !greska7_S4 || !greska7_S5 || !greska7_GS ||
        !greska8_S1 || !greska8_S2 || !greska8_S3 || !greska8_S4 || !greska8_S5 || !greska8_GS ||
        !greska9_S1 || !greska9_S2 || !greska9_S3 || !greska9_S4 || !greska9_S5 || !greska9_GS ||
        !greska10_S1 || !greska10_S2 || !greska10_S3 || !greska10_S4 || !greska10_S5 || !greska10_GS ||
        !greska11_S1 || !greska11_S2 || !greska11_S3 || !greska11_S4 || !greska11_S5 || !greska11_GS ||
        !lblZbirJunioriPrepreke) {
        console.log("Neki element grešaka nije pronađen");
        return;
    }

    // Vrednosti za greške ×10
    var g2_S1 = parseFloat(greska2_S1.value.replace(',', '.')) || 0;
    var g2_S2 = parseFloat(greska2_S2.value.replace(',', '.')) || 0;
    var g2_S3 = parseFloat(greska2_S3.value.replace(',', '.')) || 0;
    var g2_S4 = parseFloat(greska2_S4.value.replace(',', '.')) || 0;
    var g2_S5 = parseFloat(greska2_S5.value.replace(',', '.')) || 0;
    var g2_GS = parseFloat(greska2_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var g3_S1 = parseFloat(greska3_S1.value.replace(',', '.')) || 0;
    var g3_S2 = parseFloat(greska3_S2.value.replace(',', '.')) || 0;
    var g3_S3 = parseFloat(greska3_S3.value.replace(',', '.')) || 0;
    var g3_S4 = parseFloat(greska3_S4.value.replace(',', '.')) || 0;
    var g3_S5 = parseFloat(greska3_S5.value.replace(',', '.')) || 0;
    var g3_GS = parseFloat(greska3_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×20
    var g4_S1 = parseFloat(greska4_S1.value.replace(',', '.')) || 0;
    var g4_S2 = parseFloat(greska4_S2.value.replace(',', '.')) || 0;
    var g4_S3 = parseFloat(greska4_S3.value.replace(',', '.')) || 0;
    var g4_S4 = parseFloat(greska4_S4.value.replace(',', '.')) || 0;
    var g4_S5 = parseFloat(greska4_S5.value.replace(',', '.')) || 0;
    var g4_GS = parseFloat(greska4_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var g5_S1 = parseFloat(greska5_S1.value.replace(',', '.')) || 0;
    var g5_S2 = parseFloat(greska5_S2.value.replace(',', '.')) || 0;
    var g5_S3 = parseFloat(greska5_S3.value.replace(',', '.')) || 0;
    var g5_S4 = parseFloat(greska5_S4.value.replace(',', '.')) || 0;
    var g5_S5 = parseFloat(greska5_S5.value.replace(',', '.')) || 0;
    var g5_GS = parseFloat(greska5_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×5
    var g6_S1 = parseFloat(greska6_S1.value.replace(',', '.')) || 0;
    var g6_S2 = parseFloat(greska6_S2.value.replace(',', '.')) || 0;
    var g6_S3 = parseFloat(greska6_S3.value.replace(',', '.')) || 0;
    var g6_S4 = parseFloat(greska6_S4.value.replace(',', '.')) || 0;
    var g6_S5 = parseFloat(greska6_S5.value.replace(',', '.')) || 0;
    var g6_GS = parseFloat(greska6_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var g7_S1 = parseFloat(greska7_S1.value.replace(',', '.')) || 0;
    var g7_S2 = parseFloat(greska7_S2.value.replace(',', '.')) || 0;
    var g7_S3 = parseFloat(greska7_S3.value.replace(',', '.')) || 0;
    var g7_S4 = parseFloat(greska7_S4.value.replace(',', '.')) || 0;
    var g7_S5 = parseFloat(greska7_S5.value.replace(',', '.')) || 0;
    var g7_GS = parseFloat(greska7_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var g8_S1 = parseFloat(greska8_S1.value.replace(',', '.')) || 0;
    var g8_S2 = parseFloat(greska8_S2.value.replace(',', '.')) || 0;
    var g8_S3 = parseFloat(greska8_S3.value.replace(',', '.')) || 0;
    var g8_S4 = parseFloat(greska8_S4.value.replace(',', '.')) || 0;
    var g8_S5 = parseFloat(greska8_S5.value.replace(',', '.')) || 0;
    var g8_GS = parseFloat(greska8_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var g9_S1 = parseFloat(greska9_S1.value.replace(',', '.')) || 0;
    var g9_S2 = parseFloat(greska9_S2.value.replace(',', '.')) || 0;
    var g9_S3 = parseFloat(greska9_S3.value.replace(',', '.')) || 0;
    var g9_S4 = parseFloat(greska9_S4.value.replace(',', '.')) || 0;
    var g9_S5 = parseFloat(greska9_S5.value.replace(',', '.')) || 0;
    var g9_GS = parseFloat(greska9_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var g10_S1 = parseFloat(greska10_S1.value.replace(',', '.')) || 0;
    var g10_S2 = parseFloat(greska10_S2.value.replace(',', '.')) || 0;
    var g10_S3 = parseFloat(greska10_S3.value.replace(',', '.')) || 0;
    var g10_S4 = parseFloat(greska10_S4.value.replace(',', '.')) || 0;
    var g10_S5 = parseFloat(greska10_S5.value.replace(',', '.')) || 0;
    var g10_GS = parseFloat(greska10_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var g11_S1 = parseFloat(greska11_S1.value.replace(',', '.')) || 0;
    var g11_S2 = parseFloat(greska11_S2.value.replace(',', '.')) || 0;
    var g11_S3 = parseFloat(greska11_S3.value.replace(',', '.')) || 0;
    var g11_S4 = parseFloat(greska11_S4.value.replace(',', '.')) || 0;
    var g11_S5 = parseFloat(greska11_S5.value.replace(',', '.')) || 0;
    var g11_GS = parseFloat(greska11_GS.value.replace(',', '.')) || 0;

    // Izračunaj
    var zbirGresaka2 = (g2_S1 + g2_S2 + g2_S3 + g2_S4 + g2_S5 + g2_GS) * 10;
    var zbirGresaka3 = (g3_S1 + g3_S2 + g3_S3 + g3_S4 + g3_S5 + g3_GS) * 5;
    var zbirGresaka4 = (g4_S1 + g4_S2 + g4_S3 + g4_S4 + g4_S5 + g4_GS) * 20;
    var zbirGresaka5 = (g5_S1 + g5_S2 + g5_S3 + g5_S4 + g5_S5 + g5_GS) * 10;
    var zbirGresaka6 = (g6_S1 + g6_S2 + g6_S3 + g6_S4 + g6_S5 + g6_GS) * 5;
    var zbirGresaka7 = (g7_S1 + g7_S2 + g7_S3 + g7_S4 + g7_S5 + g7_GS) * 10;
    var zbirGresaka8 = (g8_S1 + g8_S2 + g8_S3 + g8_S4 + g8_S5 + g8_GS) * 10;
    var zbirGresaka9 = (g9_S1 + g9_S2 + g9_S3 + g9_S4 + g9_S5 + g9_GS) * 10;
    var zbirGresaka10 = (g10_S1 + g10_S2 + g10_S3 + g10_S4 + g10_S5 + g10_GS) * 10;
    var zbirGresaka11 = (g11_S1 + g11_S2 + g11_S3 + g11_S4 + g11_S5 + g11_GS) * 10;

    var ukupanZbirPrepreke = zbirGresaka2 + zbirGresaka3 + zbirGresaka4 + zbirGresaka5 +
        zbirGresaka6 + zbirGresaka7 + zbirGresaka8 + zbirGresaka9 +
        zbirGresaka10 + zbirGresaka11;

    lblZbirJunioriPrepreke.innerText = 'Збир грешака препреке: ' + ukupanZbirPrepreke.toFixed(2);
    lblZbirJunioriPrepreke.className = 'text-info font-weight-bold';

    izracunajUkupanPlasmanJuniori();
}

function izracunajZbirGresakaStafetaJuniori() {
    console.log("izracunajZbirGresakaStafetaJuniori pokrenut");

    // Greške ×10
    var greska2_SS = document.getElementById('Greska2_Stafeta_Starter');
    var greska2_SM1 = document.getElementById('Greska2_Stafeta_Merilac1');
    var greska2_SM2 = document.getElementById('Greska2_Stafeta_Merilac2');
    var greska2_StS = document.getElementById('Greska2_Stafeta_StazniSudija');
    var greska2_GS = document.getElementById('Greska2_Stafeta_GlavniSudija');

    // Greške ×10
    var greska3_SS = document.getElementById('Greska3_Stafeta_Starter');
    var greska3_SM1 = document.getElementById('Greska3_Stafeta_Merilac1');
    var greska3_SM2 = document.getElementById('Greska3_Stafeta_Merilac2');
    var greska3_StS = document.getElementById('Greska3_Stafeta_StazniSudija');
    var greska3_GS = document.getElementById('Greska3_Stafeta_GlavniSudija');

    // Greške ×10
    var greska4_SS = document.getElementById('Greska4_Stafeta_Starter');
    var greska4_SM1 = document.getElementById('Greska4_Stafeta_Merilac1');
    var greska4_SM2 = document.getElementById('Greska4_Stafeta_Merilac2');
    var greska4_StS = document.getElementById('Greska4_Stafeta_StazniSudija');
    var greska4_GS = document.getElementById('Greska4_Stafeta_GlavniSudija');

    var lblZbirGresakaStafetaJuniori = document.getElementById('lblZbirGresakaStafetaJuniori');

    if (!greska2_SS || !greska2_SM1 || !greska2_SM2 || !greska2_StS || !greska2_GS ||
        !greska3_SS || !greska3_SM1 || !greska3_SM2 || !greska3_StS || !greska3_GS ||
        !greska4_SS || !greska4_SM1 || !greska4_SM2 || !greska4_StS || !greska4_GS ||
        !lblZbirGresakaStafetaJuniori) {
        console.log("Neki element štafete nije pronađen");
        return;
    }

    // Vrednosti za greške ×10
    var v2_SS = parseFloat(greska2_SS.value.replace(',', '.')) || 0;
    var v2_SM1 = parseFloat(greska2_SM1.value.replace(',', '.')) || 0;
    var v2_SM2 = parseFloat(greska2_SM2.value.replace(',', '.')) || 0;
    var v2_StS = parseFloat(greska2_StS.value.replace(',', '.')) || 0;
    var v2_GS = parseFloat(greska2_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var v3_SS = parseFloat(greska3_SS.value.replace(',', '.')) || 0;
    var v3_SM1 = parseFloat(greska3_SM1.value.replace(',', '.')) || 0;
    var v3_SM2 = parseFloat(greska3_SM2.value.replace(',', '.')) || 0;
    var v3_StS = parseFloat(greska3_StS.value.replace(',', '.')) || 0;
    var v3_GS = parseFloat(greska3_GS.value.replace(',', '.')) || 0;

    // Vrednosti za greške ×10
    var v4_SS = parseFloat(greska4_SS.value.replace(',', '.')) || 0;
    var v4_SM1 = parseFloat(greska4_SM1.value.replace(',', '.')) || 0;
    var v4_SM2 = parseFloat(greska4_SM2.value.replace(',', '.')) || 0;
    var v4_StS = parseFloat(greska4_StS.value.replace(',', '.')) || 0;
    var v4_GS = parseFloat(greska4_GS.value.replace(',', '.')) || 0;

    // Izračunaj
    var zbirGresaka2 = (v2_SS + v2_SM1 + v2_SM2 + v2_StS + v2_GS) * 10;
    var zbirGresaka3 = (v3_SS + v3_SM1 + v3_SM2 + v3_StS + v3_GS) * 10;
    var zbirGresaka4 = (v4_SS + v4_SM1 + v4_SM2 + v4_StS + v4_GS) * 10;

    var ukupanZbirStafeta = zbirGresaka2 + zbirGresaka3 + zbirGresaka4;

    lblZbirGresakaStafetaJuniori.innerText = 'Збир грешака штафете: ' + ukupanZbirStafeta.toFixed(2);
    lblZbirGresakaStafetaJuniori.className = 'text-info font-weight-bold';

    izracunajUkupanPlasmanJuniori();
}

function izracunajUkupanPlasmanJuniori() {
    console.log("izracunajUkupanPlasmanJuniori pokrenut");

    var lblPocetniBodovi = document.getElementById('lblPocetniBodoviJuniori');
    var lblSrednjaPrepreka = document.getElementById('lblSrednjaVrednostJunioriPrepreke');
    var lblSrednjaStafeta = document.getElementById('lblSrednjaVrednostJunioriStafeta');
    var lblZbirPrepreka = document.getElementById('lblZbirGresakaJunioriPrepreke');
    var lblZbirStafeta = document.getElementById('lblZbirGresakaStafetaJuniori');
    var lblUkupanPlasman = document.getElementById('lblUkupanPlasmanJuniori');

    if (!lblPocetniBodovi || !lblSrednjaPrepreka || !lblSrednjaStafeta ||
        !lblZbirPrepreka || !lblZbirStafeta || !lblUkupanPlasman) {
        console.log("Neke labele nisu pronađene");
        return;
    }

    var pocetniBodovi = ekstrahujBrojIzTeksta(lblPocetniBodovi.innerText);
    var srednjaPrepreka = ekstrahujBrojIzTeksta(lblSrednjaPrepreka.innerText);
    var srednjaStafeta = ekstrahujBrojIzTeksta(lblSrednjaStafeta.innerText);
    var zbirPrepreka = ekstrahujBrojIzTeksta(lblZbirPrepreka.innerText);
    var zbirStafeta = ekstrahujBrojIzTeksta(lblZbirStafeta.innerText);

    var ukupanPlasman = pocetniBodovi - (srednjaPrepreka + srednjaStafeta + zbirPrepreka + zbirStafeta);

    lblUkupanPlasman.innerText = 'Укупан резултат: ' + ukupanPlasman.toFixed(2);

    if (ukupanPlasman < 0) {
        lblUkupanPlasman.className = 'text-danger font-weight-bold';
    } else {
        lblUkupanPlasman.className = 'text-success font-weight-bold';
    }
}

// Pomocna funkcija za ekstrakciju broja iz teksta
function ekstrahujBrojIzTeksta(text) {
    var match = text.match(/(\d+\.?\d*)/);
    return match ? parseFloat(match[0]) : 0;
}

function initJuniori() {
    console.log("initJuniori pokrenut");

    // Event listeneri za PREPREKU - vremena
    var vps1 = document.getElementById('VremePrepreke_Sudija1');
    var vps2 = document.getElementById('VremePrepreke_Sudija2');
    var vpgs = document.getElementById('VremePrepreke_GlavniSudija');

    if (vps1) vps1.addEventListener('input', izracunajSrednjuVrednostJunioriPrepreke);
    if (vps2) vps2.addEventListener('input', izracunajSrednjuVrednostJunioriPrepreke);
    if (vpgs) vpgs.addEventListener('input', izracunajSrednjuVrednostJunioriPrepreke);

    // Event listeneri za ŠTAFETU - vremena
    var vss1 = document.getElementById('VremeStafete_Sudija1');
    var vss2 = document.getElementById('VremeStafete_Sudija2');
    var vsgs = document.getElementById('VremeStafete_GlavniSudija');

    if (vss1) vss1.addEventListener('input', izracunajSrednjuVrednostJunioriStafeta);
    if (vss2) vss2.addEventListener('input', izracunajSrednjuVrednostJunioriStafeta);
    if (vsgs) vsgs.addEventListener('input', izracunajSrednjuVrednostJunioriStafeta);

    // Event listeneri za greške prepreka
    var greskeInputsPrepreke = document.querySelectorAll('input[id*="Prep_"]');
    greskeInputsPrepreke.forEach(function (input) {
        input.addEventListener('input', izracunajZbirGresakaJunioriPrepreke);
    });

    // Event listeneri za greške štafeta
    var greskeInputsStafeta = document.querySelectorAll('input[id*="Stafeta_"]');
    greskeInputsStafeta.forEach(function (input) {
        input.addEventListener('input', izracunajZbirGresakaStafetaJuniori);
    });

    // Inicijalno izračunavanje
    izracunajSrednjuVrednostJunioriPrepreke();
    izracunajSrednjuVrednostJunioriStafeta();
    izracunajZbirGresakaJunioriPrepreke();
    izracunajZbirGresakaStafetaJuniori();
    izracunajUkupanPlasmanJuniori();
}

// Pokreni kada se stranica učita
document.addEventListener('DOMContentLoaded', initJuniori);