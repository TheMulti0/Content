`Note: This is an uncompleted project.`

# Content

The purpose of this project is to consume content (currently news only) from many different sources.

Content periodically polls all of the newssources and pushes the new newsitems into the database (MongoDB and InMemory are the current implementations).

A REST API is avaliable to query from the database.

[Content also has a simple React client!](https://github.com/TheMulti0/content-client)

### Current implementation:
Database:
 - [x] MongoDB
 - [x] InMemory
 
NewsSources:
 - [x] Calcalist
 - [x] CalcalistReports
 - [x] Galatz
 - [x] GalatzReports
 - [x] Haaretz
 - [x] Kan
 - [x] KanReports
 - [x] Mako
 - [x] MakoReporters
 - [x] N0404
 - [x] TheMarker
 - [x] Walla
 - [x] WallaReports
 - [x] Ynet
 - [x] YnetReports
