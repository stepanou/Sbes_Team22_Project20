
Team Members:

 1. Kosta Markovic PR9/2018
 2. Atila Arvai PR131/2016
 4. Ognjen Pekec PR19/2018
 5. Stefan Milic PR146/2016





Projektni zadatak 20.


Razviti sistem za obradu merenja pametnog brojila i čuvanje podataka o očitanoj potrošnji. Za
svako pametno brojilo, servis čuva u bazi podataka (implementirana kao tekstualna datoteka)
sledeće informacije:

    ● jedinstveni identifikator brojila (8-cifren broj)
  
    ● ime i prezime vlasnika brojila
  
    ● utrošena električna energija.
  
  
Proizvoljan broj klijenata se prijavljuje sa zahtevom za obračunom potrošnje električne energije
(obračun se vrši po zonama: opseg zona u kWh i cena za svaku zonu su konfigurabilne vrednosti
i čuvaju se u posebnom fajlu).
Dodatno, postoje tri kategorije privilegovanih korisnika:

    ● Operatori koji imaju pravo da modifikuju vrednost utrošene električne energije ili ID
      pametnog brojila u bazi podataka (npr. u slučaju kvara na brojilu ili zamene brojila)
    
    ● Administratori koji imaju pravo da dodaju novi entitet u bazu podataka, odnosno da brišu
      postojeći iz baze, ali nemaju pravo da modifikuju postojeće vrednosti.
    
    ● Super-administratori koji imaju pravo da obrišu celu bazu podataka, kao i da rade njeno
      arhiviranje (nemaju pravo da izvršavaju operacije nad podacima u bazi).
    
    
Serverska komponenta se sastoji od dve pod-komponente: prva komponenta koja prima sve
zahteve, vrši autentifikaciju korisnika i proveru prava pristupa, i druga koja ima ulogu Load
Balancera (LB). LB komponenta se koristi za obradu zahteva potrošača, i zahteve za obračun
potrošnje prosleđuje na obradu jednoj od slobodnih Worker (W) komponenti. LB dobija
informaciju o broju W kojim raspolaže tako što se oni prilikom startovanja prijavljuju LB
komponenti, odnosno prilikom zaustavljanja odjavljuju sa LB komponente.


Komunikacija se odvija preko Windows autentifikacionog protokola, osim između LB i W
komponenti gde se koriste sertifikati po pravilu lanca poverenja.


Klijentski zahtevi ka servisu treba da budu kriptovani AES algoritmom u ECB modu.
Implementirati RBAC model kontrole pristupa koji se zasniva na Windows korisničkim grupama
i korisnicima. 

Provera prava pristupa na servisu se zasniva na proveri permisija. Veze grupa i
permisija se konfigurišu u okviru posebnog fajla.


Sve pokušaje uspešne i neuspešne autentifikacije i autorizacije je potrebno logovati u custom
kreiran Windows Event Log.
