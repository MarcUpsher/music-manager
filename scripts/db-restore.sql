USE master;
GO

/*
USE MusicManager;
GO
DROP TABLE dbo.ImageRef
GO
DROP TABLE dbo.AlbumGenre
GO
DROP TABLE dbo.Genre
GO
DROP TABLE dbo.Track
GO
DROP TABLE dbo.Album
GO
DROP TABLE dbo.Artist
GO

*/

/*
select * from Artist
select * from Album
select * from AlbumTracks
*/

CREATE DATABASE MusicManager;
--USE MusicManager;
GO

CREATE TABLE dbo.ImageRef (
  ImageId int IDENTITY(1,1) PRIMARY KEY,
  URI varchar(500) null,
  DateAdded DATETIME not null DEFAULT GETDATE(),
  DateModified DATETIME null,
  DateDeleted DATETIME null
)
GO

CREATE TABLE dbo.Genre (
  GenreId int IDENTITY(1,1) PRIMARY KEY,
  Name varchar(500) null,
  DateAdded DATETIME not null DEFAULT GETDATE(),
  DateModified DATETIME null,
  DateDeleted DATETIME null
)
GO

CREATE TABLE dbo.Artist (
  ArtistId int IDENTITY(1,1) PRIMARY KEY,
  Name varchar(500) null,
  DateAdded DATETIME not null DEFAULT GETDATE(),
  DateModified DATETIME null,
  DateDeleted DATETIME null
)
GO

CREATE TABLE dbo.Album (
  AlbumId int IDENTITY(1,1) PRIMARY KEY,
  ArtistId int not null,
  Name varchar(500) null,
  DateReleased DateTime null,
  Summary varchar(2000),  
  DateAdded DATETIME not null DEFAULT GETDATE(),
  DateModified DATETIME null,
  DateDeleted DATETIME null,
  CONSTRAINT Fk_Album_Artist_Id
    FOREIGN KEY (ArtistId)
    REFERENCES Artist (ArtistId)
)
GO

CREATE TABLE dbo.Track (
  TrackId int IDENTITY(1,1) PRIMARY KEY,
  AlbumId int not null,
  Position int not null,
  Name varchar(500) null,
  Duration int,  
  DateAdded DATETIME not null DEFAULT GETDATE(),
  DateModified DATETIME null,
  DateDeleted DATETIME null,
  CONSTRAINT Fk_Track_Album_Id
    FOREIGN KEY (AlbumId)
    REFERENCES Album (AlbumId)
)
GO

CREATE TABLE dbo.AlbumGenre (
  AlbumGenreId int IDENTITY(1,1) PRIMARY KEY,
  AlbumId int not null,
  GenreId int not null,
  DateAdded DATETIME not null DEFAULT GETDATE(),
  DateDeleted DATETIME null,
  CONSTRAINT Fk_AlbumGenre_Album_Id
    FOREIGN KEY (AlbumId)
    REFERENCES Album (AlbumId),
  CONSTRAINT Fk_AlbumGenere_Genre_Id
    FOREIGN KEY (GenreId)
    REFERENCES Genre (GenreId)
)
GO

-- Insert genres
INSERT INTO [dbo].[Genre] ([Name]) VALUES ('Rock')
INSERT INTO [dbo].[Genre] ([Name]) VALUES ('Metal')
INSERT INTO [dbo].[Genre] ([Name]) VALUES ('Pop')
GO

-- Insert artists
INSERT INTO [dbo].[Artist] ([Name]) VALUES ('The Night Flight Orchestra')
GO

-- Insert albums
INSERT INTO [dbo].[Album] ([ArtistId], [Name], [DateReleased], [Summary])
VALUES (
	1
	,'Internal Affairs'
	,cast('2012-06-18' as datetime),
	'Internal Affairs is the first studio album by Swedish classic rock/AOR band The Night Flight Orchestra, released on 18 June 2012 via Coroner Records.'
	)
GO

--Insert album/genre link
INSERT INTO [dbo].[AlbumGenre] ([AlbumId] ,[GenreId]) VALUES (1,1)
INSERT INTO [dbo].[AlbumGenre] ([AlbumId] ,[GenreId]) VALUES (1,3)
GO

-- Insert tracks
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,1,'Siberian Queen', 363)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,2,'California Morning', 337)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,3,'Glowing City Madness', 237)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,4,'West Ruth Ave', 402)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,5,'Transatlantic Blues', 501)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,6,'Miami 5:02', 229)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,7,'Internal Affairs', 298)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,8,'1998', 297)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,9,'Stella Ain''t No Dove', 267)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,10,'Montreal Midnight Supply', 281)
INSERT INTO [dbo].[Track] ([AlbumId], [Position], [Name], [Duration])
VALUES (1,11,'Green Hills of Glumslöv', 248)


