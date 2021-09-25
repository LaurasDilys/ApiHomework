# ApiHomework

Sukurti loginimo API kuris apima:
* 1. Log'o išvedimą į konsolę
* 2. Log'o išsiuntimą el.paštu
* 3. Log'o įrašymą į failą
* 4. Log'o įrašymą į sql db (repository patternas nereikalingas)
* API konfigūracijoje nustatmas koks log įrašymo mechanizmas (1,2,3 ar 4) bus naudojamas
* Turi būti realizuota api mechanizmai:
*    - žinutės įrašymo,
*    - žinučių sąrašo skaitymo,
*    - vienos žinutės skaitymo.  

* Darbas vykdomas per GitHub taip, kad matytųsi kiekvieno asmens įdirbis (dirbama skirtingose šakose, o rezultatas merdžinamas į master)
* Turi būti realizuotas tik vienas endpointas "api/logs"
* Naudoti OOP architektūros principus
* Naudoti SOLID architektūros principus

Prisiminkite, kad DTO ir biznio modeliai gali ir nesutapti.
LOG'o pranešimo (request) json DTO pavyzdys:
```
{
  "events": [
    {
      "Timestamp": "2016-11-03T00:09:11.4899425+01:00",
      "Level": "Information",
      "MessageTemplate": "Logging {@Heartbeat} from {Computer}",
      "RenderedMessage": "Logging { UserName: \"Mike\", UserDomainName: \"Home\" } from \"Workstation\"",
      "Properties": {
        "Heartbeat": {
          "UserName": "Mike",
          "UserDomainName": "Home"
        },
        "Computer": "Workstation",
        "Parameters": [],
        "Retries": 3
      }
    },
    {
      "Timestamp": "2016-11-03T00:09:12.4905685+01:00",
      "Level": "Information",
      "MessageTemplate": "Logging {@Heartbeat} from {Computer}",
      "RenderedMessage": "Logging { UserName: \"Mike\", UserDomainName: \"Home\" } from \"Workstation\"",
      "Properties": {
        "Heartbeat": {
          "UserName": "Mike",
          "UserDomainName": "Home"
        },
        "Computer": "Workstation",
        "Parameters": []
      }
    }
  ]
}
```
