# Calculator
#### tým:

    \_(*_*)_/\_(*_*)_/\_(*_*)_/\_(*_*)_/
#### členové:
* xbacae00
* xskvar09
* xzavad18
* xnovot2a 

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
- [ ] Profiling
- [ ] Instalačka

Nerozděleno:
- [x] Verzování (git)
- [x] **Nasdílet repozitář někomu z ivs**
- [ ] Screen jak debugujem

## Instalace
poprosím doplnit mistra kubíka

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





