CREATE DATABASE [bd-Taboada-Tp4];
GO
USE [bd-Taboada-Tp4];
GO
CREATE TABLE [bd-Taboada-Tp4].dbo.clientes
    (
    dni VARCHAR(60) NOT NULL PRIMARY KEY ,
    nombre VARCHAR(60) NOT NULL,
    apellido VARCHAR(60) NOT NULL,
    telefono VARCHAR(60) NOT NULL ,    
    );
GO
Insert into clientes VALUES ('25788814','Jose Luis','Cardenas','45859696')
Insert into clientes VALUES ('16787414','Maria Luisa','Cardenas','42462821')
Insert into clientes VALUES ('36258814','Esteban Javier','Viola','53232987')
Insert into clientes VALUES ('41088800','Lorenzo Juan','Videla','45186832')
Insert into clientes VALUES ('21588885','Luis Alberto','Alvarez','48401383')
Insert into clientes VALUES ('42188854','Miguel Alberto','Lugo','45053619')
Insert into clientes VALUES ('34788822','Mario Jose','Rivas','43194340')
Insert into clientes VALUES ('45788815','Laura Edith','Perez','44133786')
Insert into clientes VALUES ('35788810','Mariana Victoria','Gomez','42344567')
Insert into clientes VALUES ('42088802','Cristina Celeste','Sanchez','46424129')
