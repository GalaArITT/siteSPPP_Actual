USE sitio_sepla
GO
go

/*==============================================================*/
/* Table: DEPARTAMENTOS                                         */
/*==============================================================*/
create table DEPARTAMENTOS (
   IDDEPARTAMENTO       int                  identity,
   NOMBREDEPTO          nvarchar(100)        null,
   constraint PK_DEPARTAMENTOS primary key (IDDEPARTAMENTO)
)
go

/*==============================================================*/
/* Table: INFORMACION                                           */
/*==============================================================*/
create table INFORMACION (
   IDINFORMACION        int                  not null,
   ANTECEDENTES         nvarchar(300)        null,
   MISION               nvarchar(300)        null,
   VISION               nvarchar(200)        null,
   OBJETIVO             nvarchar(200)        null,
   constraint PK_INFORMACION primary key (IDINFORMACION)
)
go

/*==============================================================*/
/* Table: NOTICIAS_SEPLAN                                       */
/*==============================================================*/
create table NOTICIAS_SEPLAN (
   IDNOTICIA            int                  identity,
   IDUSUARIO            int                  null,
   TITULO               nvarchar(Max)        null,
   ENCABEZADO           nvarchar(Max)        null,
   FECHAPUBLIC          datetime2            null,
   RESPONSABLE          nvarchar(150)        null,
   BALAZO1              nvarchar(200)        null,
   BALAZO2              nvarchar(200)        null,
   BALAZO3              nvarchar(200)        null,
   VER_COPLADENAY       nvarchar(1)          null,
   VER_SEPLAN           nvarchar(1)          null,
   VER_INTRANET         nvarchar(1)          null,
   LUGAR                nvarchar(150)        null,
   PRIORIDAD            numeric(2,0)         null,
   SOLO_MEDIOS          nvarchar(1)          null,
   PARTICIPANTES        nvarchar(300)        null,
   RESENIA              nvarchar(400)        null,
   FOTO_PRINCIPAL       varbinary(max)       null,
   PIE_FOTO_PRINCIPAL   nvarchar(300)        null,
   FOTO_2               varbinary(max)       null,
   PIE_FOTO_2           nvarchar(300)        null,
   FOTO_3               varbinary(max)       null,
   PIE_FOTO_3           nvarchar(300)        null,
   FOTO_4               varbinary(max)       null,
   PIE_FOTO_4           nvarchar(300)        null,
   FOTO_5               varbinary(max)       null,
   PIE_FOTO_5           nvarchar(300)        null,
   FOTO_6               varbinary(max)       null,
   PIE_FOTO_6           nvarchar(300)        null,
   constraint PK_NOTICIAS_SEPLAN primary key (IDNOTICIA)
)
go

/*==============================================================*/
/* Table: SERVIDORESPUBLICOS                                    */
/*==============================================================*/
create table SERVIDORESPUBLICOS (
   IDSERVPUB            int                  identity,
   IDDEPARTAMENTO       int                  null,
   NOMBREPERSONAL       nvarchar(300)        null,
   NOMBRAMIENTO         nvarchar(100)        null,
   CONMUTADOR           nvarchar(20)         null,
   EXT                  nvarchar(3)          null,
   FOTOPERSONAL         varbinary(Max)       null,
   CORREO               nvarchar(50)         null,
   CURRICULUM           varbinary(Max)       null,
   ESTATUS              nchar(1)             null,
   constraint PK_SERVIDORESPUBLICOS primary key (IDSERVPUB)
)
go

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   IDUSUARIO            int                  identity,
   USUARIOINICIA        nvarchar(50)         null,
   CONTRASENA           nvarchar(50)         null,
   NOMBREUSUARIO        nvarchar(400)        null,
   ROL                  tinyint              null,
   ESTATUS              nvarchar(1)          null,
   constraint PK_USUARIO primary key (IDUSUARIO)
)
go

alter table NOTICIAS_SEPLAN
   add constraint FK_NOTICIAS_REFERENCE_USUARIO foreign key (IDUSUARIO)
      references USUARIO (IDUSUARIO)
go

alter table SERVIDORESPUBLICOS
   add constraint FK_SERVIDORES_DEPTO foreign key (IDDEPARTAMENTO)
      references DEPARTAMENTOS (IDDEPARTAMENTO)
go

select * from USUARIO
/*
	Tipos: 
	1 = Capturista
	2 = Administrador

	Estatus
	A= Activo
	I= Inactivo
*/
INSERT INTO USUARIO
VALUES ('123','123', 'USUARIO CAPTURISTA',2,'A')

update USUARIO
set NOMBREUSUARIO = 'USUARIO CAPTURISTA'
where IDUSUARIO=2