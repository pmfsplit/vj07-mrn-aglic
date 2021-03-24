# vj07-mrn-aglic
vj07-mrn-aglic created by GitHub Classroom

## AkkaClusterExample primjer
Prebačen primjer sa vježbi u .NET core. Primjer demonstrira kako je jednostavno napraviti klaster od N actorsustava (N >= 2). 
Actori u actor sustavu su pretplačeni na određen broj događaja.

Primjer sadrži MyAkkaConfig klasu kako bi se json mapirao u objekt kojega možemo koristiti prilikom stvaranja actor sustava. 

### Pokretanje
Iz terminala (terminal, powershell, cmd...):

.NET Framework: `AkkaClusterExample.exe PORT` (potrebno je navesti PORT samo za seed-nodeove. Ako se port ne navede, program automatski postavlja port 0,
  što signalizira actor sustavu da odabere slučajni slobodni port.
  
.NET Core: pozicionirat se u folder od PROJEKTA. Naredba: `dotnet run` (-- PORT -> navodi se samo ako je potrebno dodijeliti port, kao u slučaju seed-nodeova).

Za one koji koriste Visual Studio: Visual Studio bi trebao imati opciju da pokrenete novu instancu nego projekta desnim klikom na projekt. 
