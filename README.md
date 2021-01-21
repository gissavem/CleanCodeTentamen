# CleanCodeTentamen - John Andersson


### Vad du valt att testa och varför?

Jag har valt att testa sorteringen och metoden som skall se till att ta bort dubletter av titlar.
Jag tyckte det kändes naturligt att testa just den delen av programmet då det egentligen var den enda biten som kunde enhetstestas på ett smidigt och effektivt sätt.
Med hjälp av Moq ersatte jag tjänsten som hämtade den faktiskta datan med en metod som hämtade mockdata, på så sätt slipper jag tänka på om det externa API:et fungerar eller inte.

### Vilket/vilka designmönster har du valt, varför? Hade det gått att göra på ett annat sätt?

Jag använder Singleton genom dependency injection som mitt designmönster för att det var smidigt och enkelt sätt att separera mina controllers och min logik.
Eftersom det var en ganska simpel övning kändes det inte som att något mer avancerat mönster behövdes.

### Hur mycket valde du att optimera koden, varför är det en rimlig nivå för vårt program?

Jag fokuserade på att göra koden så lättläslig och clean i första hand och funderade inte så mycket på om sorteringsalgoritmen som LINQ använder sig av var optimal eller inte. I ett programm av den här typen kan det givetvis vara vettigt att fundera på sorteringen, om datasetet hade bestått av hundratusentals filmer istället för strax över 100.

I det här fallet kändes det dock utanför scope.
