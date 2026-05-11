// Profesionalci.js - Kompletan JavaScript kod

function izracunajSveStatistike() {
    console.log("izracunajSveStatistike pokrenut");

    izracunajSrednjuVrednostProfesionalciMvp();
    izracunajSrednjuVrednostProfesionalciStafeta();
    izracunajZbirGresakaProfesionalciMvp();
    izracunajZbirGresakaProfesionalciStafeta();
    izracunajUkupanPlasmanProfesionalci();
}

function izracunajSrednjuVrednostProfesionalciMvp() {
    console.log("izracunajSrednjuVrednostProfesionalciMvp pokrenut");

    var vms1 = document.getElementById('VremeMVP_Sudija1');
    var vms2 = document.getElementById('VremeMVP_Sudija2');
    var vms3 = document.getElementById('VremeMVP_Sudija3');
    var vmgs = document.getElementById('VremeMVP_GlavniSudija');
    var lblSrednjaProfesionalciMvp = document.getElementById('lblSrednjaVrednostProfesionalciMvp');

    if (!vms1 || !vms2 || !vms3 || !vmgs || !lblSrednjaProfesionalciMvp) {
        console.log("Neki element MVP nije pronađen");
        return;
    }

    var vms1_val = parseFloat(vms1.value.replace(',', '.')) || 0;
    var vms2_val = parseFloat(vms2.value.replace(',', '.')) || 0;
    var vms3_val = parseFloat(vms3.value.replace(',', '.')) || 0;
    var vmgs_val = parseFloat(vmgs.value.replace(',', '.')) || 0;

    console.log("MVP vrednosti:", vms1_val, vms2_val, vms3_val, vmgs_val);

    var brojValidnih = 0;
    var suma = 0;

    if (vms1_val > 0) { suma += vms1_val; brojValidnih++; }
    if (vms2_val > 0) { suma += vms2_val; brojValidnih++; }
    if (vms3_val > 0) { suma += vms3_val; brojValidnih++; }
    if (vmgs_val > 0) { suma += vmgs_val; brojValidnih++; }

    var srednja = brojValidnih > 0 ? (suma / brojValidnih) : 0;
    lblSrednjaProfesionalciMvp.innerText = 'Средња време препреке: ' + srednja.toFixed(2);
    console.log("MVP srednja:", srednja);
}

function izracunajSrednjuVrednostProfesionalciStafeta() {
    console.log("izracunajSrednjuVrednostProfesionalciStafeta pokrenut");

    var vss1 = document.getElementById('VremeStafetaMVP_SST');
    var vss2 = document.getElementById('VremeStafetaMVP_SMV');
    var vsgs = document.getElementById('VremeStafetaMVP_GlavniSudija');
    var lblSrednjaProfesionalciStafeta = document.getElementById('lblSrednjaVrednostProfesionalciStafeta');

    if (!vss1 || !vss2 || !vsgs || !lblSrednjaProfesionalciStafeta) {
        console.log("Neki element štafete nije pronađen");
        return;
    }

    var vss1_val = parseFloat(vss1.value.replace(',', '.')) || 0;
    var vss2_val = parseFloat(vss2.value.replace(',', '.')) || 0;
    var vsgs_val = parseFloat(vsgs.value.replace(',', '.')) || 0;

    console.log("Štafeta vrednosti:", vss1_val, vss2_val, vsgs_val);

    var brojValidnih = 0;
    var suma = 0;

    if (vss1_val > 0) { suma += vss1_val; brojValidnih++; }
    if (vss2_val > 0) { suma += vss2_val; brojValidnih++; }
    if (vsgs_val > 0) { suma += vsgs_val; brojValidnih++; }

    var srednja = brojValidnih > 0 ? (suma / brojValidnih) : 0;
    lblSrednjaProfesionalciStafeta.innerText = 'Средња вредност штафете: ' + srednja.toFixed(2);
    console.log("Štafeta srednja:", srednja);
}

function izracunajZbirGresakaProfesionalciMvp() {
    console.log("izracunajZbirGresakaProfesionalciMvp pokrenut");

    var ukupanZbir = 0;

    // Greške ×5
    ukupanZbir += izracunajGreskuMVP('Greska2', 5);
    ukupanZbir += izracunajGreskuMVP('Greska3', 5);
    ukupanZbir += izracunajGreskuMVP('Greska4', 5);
    ukupanZbir += izracunajGreskuMVP('Greska5', 5);
    ukupanZbir += izracunajGreskuMVP('Greska6', 5);
    ukupanZbir += izracunajGreskuMVP('Greska7', 5);
    ukupanZbir += izracunajGreskuMVP('Greska8', 5);

    // Greške ×10
    ukupanZbir += izracunajGreskuMVP('Greska9', 10);
    ukupanZbir += izracunajGreskuMVP('Greska10', 10);
    ukupanZbir += izracunajGreskuMVP('Greska11', 10);
    ukupanZbir += izracunajGreskuMVP('Greska12', 10);
    ukupanZbir += izracunajGreskuMVP('Greska13', 10);
    ukupanZbir += izracunajGreskuMVP('Greska14', 10);

    // Greške ×20
    ukupanZbir += izracunajGreskuMVP('Greska15', 20);
    ukupanZbir += izracunajGreskuMVP('Greska16', 20);

    var lblZbirGresakaProfesionalciMvp = document.getElementById('lblZbirGresakaProfesionalciMvp');
    if (lblZbirGresakaProfesionalciMvp) {
        lblZbirGresakaProfesionalciMvp.innerText = 'Збир грешака препреке: ' + ukupanZbir.toFixed(2);
        lblZbirGresakaProfesionalciMvp.className = 'text-info font-weight-bold';
    }

    console.log("Zbir grešaka MVP:", ukupanZbir);
}

function izracunajGreskuMVP(greskaNaziv, multiplikator) {
    var greskaS1 = parseFloat(document.getElementById(greskaNaziv + '_MVP_Sudija1')?.value || 0);
    var greskaS2 = parseFloat(document.getElementById(greskaNaziv + '_MVP_Sudija2')?.value || 0);
    var greskaS3 = parseFloat(document.getElementById(greskaNaziv + '_MVP_Sudija3')?.value || 0);
    var greskaGS = parseFloat(document.getElementById(greskaNaziv + '_MVP_GlavniSudija')?.value || 0);

    var zbirGresaka = greskaS1 + greskaS2 + greskaS3 + greskaGS;
    return zbirGresaka * multiplikator;
}

function izracunajZbirGresakaProfesionalciStafeta() {
    console.log("izracunajZbirGresakaProfesionalciStafeta pokrenut");

    var ukupanZbir = 0;

    // Greške ×5
    ukupanZbir += izracunajGreskuStafeta('Greska2', 5);
    ukupanZbir += izracunajGreskuStafeta('Greska3', 5);

    // Greške ×10
    ukupanZbir += izracunajGreskuStafeta('Greska4', 10);

    // Greške ×20
    ukupanZbir += izracunajGreskuStafeta('Greska5', 20);
    ukupanZbir += izracunajGreskuStafeta('Greska6', 20);

    var lblZbirGresakaProfesionalciStafeta = document.getElementById('lblZbirGresakaProfesionalciStafeta');
    if (lblZbirGresakaProfesionalciStafeta) {
        lblZbirGresakaProfesionalciStafeta.innerText = 'Збир грешака штафете: ' + ukupanZbir.toFixed(2);
        lblZbirGresakaProfesionalciStafeta.className = 'text-info font-weight-bold';
    }

    console.log("Zbir grešaka štafete:", ukupanZbir);
}

function izracunajGreskuStafeta(greskaNaziv, multiplikator) {
    var greskaSST = parseFloat(document.getElementById(greskaNaziv + '_StafetaMVP_SST')?.value || 0);
    var greskaSMV = parseFloat(document.getElementById(greskaNaziv + '_StafetaMVP_SMV')?.value || 0);
    var greskaST = parseFloat(document.getElementById(greskaNaziv + '_StafetaMVP_ST')?.value || 0);
    var greskaGS = parseFloat(document.getElementById(greskaNaziv + '_StafetaMVP_GlavniSudija')?.value || 0);

    var zbirGresaka = greskaSST + greskaSMV + greskaST + greskaGS;
    return zbirGresaka * multiplikator;
}

function izracunajUkupanPlasmanProfesionalci() {
    console.log("izracunajUkupanPlasmanProfesionalci pokrenut");

    var lblPocetniBodovi = document.getElementById('lblPocetniBodoviProfesionalci');
    var lblSrednjaMvp = document.getElementById('lblSrednjaVrednostProfesionalciMvp');
    var lblSrednjaStafeta = document.getElementById('lblSrednjaVrednostProfesionalciStafeta');
    var lblZbirMvp = document.getElementById('lblZbirGresakaProfesionalciMvp');
    var lblZbirStafeta = document.getElementById('lblZbirGresakaProfesionalciStafeta');
    var lblUkupanPlasman = document.getElementById('lblUkupanPlasmanProfesionalci');

    if (!lblPocetniBodovi || !lblSrednjaMvp || !lblSrednjaStafeta ||
        !lblZbirMvp || !lblZbirStafeta || !lblUkupanPlasman) {
        console.log("Neke labele nisu pronađene");
        return;
    }

    var pocetniBodovi = ekstrahujBrojIzTeksta(lblPocetniBodovi.innerText);
    var srednjaMvp = ekstrahujBrojIzTeksta(lblSrednjaMvp.innerText);
    var srednjaStafeta = ekstrahujBrojIzTeksta(lblSrednjaStafeta.innerText);
    var zbirMvp = ekstrahujBrojIzTeksta(lblZbirMvp.innerText);
    var zbirStafeta = ekstrahujBrojIzTeksta(lblZbirStafeta.innerText);

    var ukupanPlasman = pocetniBodovi - (srednjaMvp + srednjaStafeta + zbirMvp + zbirStafeta);

    lblUkupanPlasman.innerText = 'Укупан резултат: ' + ukupanPlasman.toFixed(2);

    if (ukupanPlasman < 0) {
        lblUkupanPlasman.className = 'text-danger font-weight-bold';
    } else {
        lblUkupanPlasman.className = 'text-success font-weight-bold';
    }

    console.log("Ukupan plasman:", ukupanPlasman);
}

function ekstrahujBrojIzTeksta(text) {
    var match = text.match(/(\d+\.?\d*)/);
    return match ? parseFloat(match[0]) : 0;
}

function initProfesionalci() {
    console.log("initProfesionalci pokrenut");

    // Proveri da li su elementi pronađeni
    var vms1 = document.getElementById('VremeMVP_Sudija1');
    var lblSrednja = document.getElementById('lblSrednjaVrednostProfesionalciMvp');

    console.log("VremeMVP_Sudija1 element:", vms1);
    console.log("Labela srednja element:", lblSrednja);

    // Event listeneri za sva polja
    var sviInputi = document.querySelectorAll('input[type="text"]');
    console.log("Pronađeno input polja:", sviInputi.length);

    sviInputi.forEach(function (input) {
        input.addEventListener('input', function () {
            console.log("Input promenjen:", input.id, "vrednost:", input.value);
            izracunajSveStatistike();
        });
    });

    // Inicijalno izračunavanje
    setTimeout(function () {
        console.log("Pokrećem inicijalno izračunavanje");
        izracunajSveStatistike();
    }, 500);
}

function izracunajSveStatistike() {
    console.log("=== izracunajSveStatistike ===");

    izracunajSrednjuVrednostProfesionalciMvp();
    izracunajSrednjuVrednostProfesionalciStafeta();
    izracunajZbirGresakaProfesionalciMvp();
    izracunajZbirGresakaProfesionalciStafeta();
    izracunajUkupanPlasmanProfesionalci();

    console.log("=== završeno izračunavanje ===");
}
// Pokreni kada se stranica učita
document.addEventListener('DOMContentLoaded', initProfesionalci);