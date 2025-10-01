# Dagboksappen

En enkel dagboksapplikation i C# (.NET 8) som körs i konsolen.  
Användaren kan skriva nya anteckningar, lista alla anteckningar, söka på datum samt spara och läsa från fil.  
Data lagras i JSON-format på disk.

---

## Hur man kör appen

### Krav
- .NET 8 SDK
- Git (för att klona repot)

### Körning
```bash
git clone <repo-url>
cd Dagboksappen/Dagboksappen
dotnet run

## Exempel på hur man kör I/O

--- Dagboksappen ---
1. Skriv ny anteckning
2. Lista alla anteckningar
3. Sök anteckning på datum
4. Spara till fil
5. Läs från fil
6. Avsluta
> 1

Ange datum (yyyy-MM-dd): 2025-09-26
Skriv din anteckning: Första anteckningen i min dagbok!
Anteckning sparad!

> 2
2025-09-26: Första anteckningen i min dagbok!

## Reflektion

Jag valde att använda en List<DiaryEntry> som datastruktur eftersom den är enkel att arbeta med när man vill lagra flera objekt av samma typ och iterera över dem.
För filformat använde jag JSON eftersom det både är lättläst för människor och enkelt att serialisera/deserialisera i C# med System.Text.Json.
För att undvika kraschande program använde jag try/catch runt filoperationer och DateTime.TryParse för datumvalidering.
Jag lade också till kontroller för tomma texter för att förhindra ogiltiga anteckningar.
En möjlig förbättring vore att använda en Dictionary<DateTime, DiaryEntry> för snabbare uppslagningar samt att logga fel till en separat error.log.
Det skulle göra programmet mer robust och enklare att bygga vidare på i framtiden.