# Calculator
#### tým:

    \_(*_*)_/\_(*_*)_/\_(*_*)_/\_(*_*)_/
#### členové:
* xbacae00
* xskvar09
* xzavad18
* xnovot2a 

#### Prostredi
* Windows 64bit

#### Licence
* Program je poskytován pod licencí GNU GPL v.3

## Co se musí dělat
Erik:
- [x] GUI a nápověda
- [ ] Mockup GUI

Honza:
- [x] Parser
- [x] Testy pro parser
- [x] Uživatelská a programová dokumentace

Michal:
- [x] Matematická knihovna
- [x] Testy pro knihovnu
- [x] Makefile 

Kuba:
- [x] Profiling
- [x] Instalačka

Nerozděleno:
- [x] Verzování (git)
- [x] **Nasdílet repozitář někomu z ivs**
- [x] Screen jak debugujem

## Instalace
* Otevři příslušný Setup.msi
* Klikni na další
* Vyber umístění složky, kam se daný program/kalkulačka má nainstalovat (výchozí složka: C:\Program Files (x86)\Calculator\Calculator\ . Možnost zvolení, zdali se nainstule pouze pro aktuálního uživatele nebo pro všechny uživatelé na počítači. Lze také zkntrolovat potřebné místo na disku pro daný program a zobrazit dostupné volné. Pro pokračování stiskněte Další
* Nyní je program může být nainstalován kliknutím na Další.
* Počkáme až se uspěšne program nainstaluje.
* Nyní mužeme kliknout na Zavřít a tím instalace je dokončena.
* Aplikaci mužeme nyní spustit s plochy a to za pomocí ikony Calculator.
 
## Odinstalace
* Odinstalaci můžeme řešit třemi způsoby
##### 1) Odisntalací za pomocí instalátoru.

* Spustíme setup.msi
* Zvolíme Odebrat sadu Calculator. (v případě problému, můžeme zvolit i Opravit sadu Calculator a tím opravit případné požkozené soubory)
*  Klikneme na Dokončit.
* Vyčkáme dokud se odinstalace neprovede.
*  Poté klikneme na Zavřít a tím je odinstalace hotová.
##### 2) Pomocí odinstalátoru v instalační složce Kalkulačky

* V průzkumníku nebo kdekoliv jinde otevřem umístění kalkulačky ketré je následující: C:\Program Files (x86)\Calculator\Calculator
* Klikneme na zástupce Uninstall
* Zobrazí se okno, klikneme na Ano
* Sečkáme než je odinstalace hotová.
* Odinstalace skončí jakmile se uzavřou všechny dotatečná okna.
* Odinstalace byla uspěšná
##### 3) Pomocí aplikace funkce obdoba Programů v kontrolním panelu.

* Vyhledáme a spustíme Aplikace a funkce
* nalezneme daný program, v tomoto případě Calculator
* Klikneme na Odinstalovat nebo na upravit (obdoba bodu 1), vyskočí okénko a povrdíme kliknutím na Odinstalovat
* Počkáme než se odinstalace dokončí.
* Po uzavření okne je aplikace uspěšne odinstalováná

## Seznam podporovaných funkcí:
* plus
* mínus
* krát
* děleno
* factoriál
* odmocnina
* mocnina
* přirozený logaritmus
* závorky

Aplikace také podporuje prioritu operátorů a závorek

## String format pro Parser
Ve třídě Parser v metodách Solve() a Validate() se bere jako parametr string s příkladem, který má třída ověřit / spočítat. Tento string by měl mít následující podobu:
* nesmí obsahovat žádné mezery
* nesmí obsahovat jiné znaky než čísla a znaky operací uvedé níže
* čísla mohou obsahovat i desetinou tečku (preferováno), i čárku
* známénko by mělo být jen jedno, pokud jich bude více bodou nahrazeny za jejich jednoznaménkový ekvivalent (-- se převede na +)
* všechny operace automaticky berou znaménko co je před číslem (-3^2 je -3 na druhou), jinak je nutno použít závorky

Pokud nastane problém, Solve() vrátí NaN, Validate() vrátí false
#### Formát operací

    Operace Příklad Výsledek
    x       -2.3    -2.3
    x+y     5+-2.1  2.9
    x-y     5-3     2
    x*y     -3*2    -6
    x/y     -2/-2   1
    x^y     -3^2    9
    x√y     9√3     3      
    x!      3!      6
    lnx     ln1     0

*Poznámka: do závorek lze dávat další závorky*




