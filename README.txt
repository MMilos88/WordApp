# WordApp
Ovo je servis za aplikaciju koja broji reci.Radjen je kao WebApi .netcore2.2 a kao baza je koriscen sqlexpress.
Iz tog razloga je potrebno zameniti connection string u fajlu  koji se nalazi u  na lokaciji WordApp/appsettings.json kao parametar nazvan SampleDatabase.
Takodje kreirano je  tako da repository sam prilikom instanciranja insertuje tekst od 55 reci i da prilikom odabira odgovarajuce akcije za bazu broji reci iz tog teksta.
