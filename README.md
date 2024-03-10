# API pro správu databáze v oblasti obchodu s templaty
Toto API slouží k manipulaci s daty v databázi v oblasti obchodu s templaty. Poskytuje rozhraní pro vkládání, mazání, čtení a úpravu záznamů v tabulkách, které obsahují informace o templatech a souvisejících entitách. 

## Struktura databáze
Databáze obsahuje následující tabulky:

- Authors
  - id (int, PK)
  - name (varchar, not null)
  - lastname (varchar, not null)
  - email (varchar, not null)
  - portfolio (string)

- Bank Transfers
  - id (int, PK)
  - variable symbol (varchar, not null)
  - iban (varchar, not null)

- Credit Cards
  - id (int, PK)
  - card number (varchar, not null)
  - expiration date (datetime, not null)
  - cvv (varchar, not null)

- Customers
  - id (int, PK)
  - name (varchar, not null)
  - lastname (varchar, not null)
  - email (varchar, not null)
  - phone (varchar, not null)
  - password (varchar, not null)

- Items
  - id (int, PK)
  - order_id (int, FK)
  - template_id (varchar, FK)
  - quantity (int, not null)
  - price of item (float, not null)

- Orders
  - id (int, PK)
  - customer_id (int, FK)
  - payment_id (int, FK)
  - order number (int, not null)
  - date (datetime, not null)
  - order_price (float, not null)

- Payments
  - id (int, PK)
  - bank_transfer_id (int, FK)
  - credit_card_id (int, FK)
  - description (varchar)

- Templates
  - id (int, PK)
  - type_id (int, FK)
  - author_id (int, FK)
  - name (varchar, not null)
  - priced (boolean, not null)
  - price (float)

- Type
  - id (int, PK)
  - nazev (varchar, not null)

## Funkce API
### Manipulace s daty
API umožňuje vložení nových entit, úpravu existujících entit, mazání entit a získání všech entit z databáze nebo konkrétní podle id. Zároveň API dohlíží na správnost (konzistenci) dat a pomáhá tak uživateli.

### Import dat
API umožňuje import dat do databáze z formátů JSON (pro třidu Author) a XML (pro třídu Customer).
Data mohou být naplněné například autory nebo customery.

### Konfigurace API
Možnost nastavení programu pomocí konfiguračního souboru (appsettings.json). Obsahuje připojení k databázi.

## Zpracování chyb
API reaguje na chyby a nekonzistence v datech prostřednictvím vhodných chybových hlášek.
V případě vzniku chyby, kterou nelze automaticky vyřešit, API vrátí odpovídající chybový kód a popis problému.

## Návrhové vzory a best practices
API využívá návrhový vzor v programování, kdy ke každé třídě existuje třída kontrolující data a komunikující s databází.

## Dokumentace a odevzdání
API obsahuje dokumentaci pro programátory, která popisuje strukturu, funkce a použité technologie ve formátu XML.

Součástí je také tento soubor README s informacemi o použití a instalaci API.

Práce je také na githubu, pod tímto odkazem: 
