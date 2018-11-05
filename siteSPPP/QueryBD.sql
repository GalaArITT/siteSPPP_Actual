USE sitio_sepla
GO
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('BALAZO') and o.name = 'FK_BALAZO_REFERENCE_NOTICIAS')
alter table BALAZO
   drop constraint FK_BALAZO_REFERENCE_NOTICIAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FOTOS') and o.name = 'FK_FOTOS_REFERENCE_NOTICIAS')
alter table FOTOS
   drop constraint FK_FOTOS_REFERENCE_NOTICIAS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('INFORMES') and o.name = 'FK_INFORMES_REFERENCE_TIPO_INF')
alter table INFORMES
   drop constraint FK_INFORMES_REFERENCE_TIPO_INF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('INFORMES') and o.name = 'FK_INFORMES_REFERENCE_USUARIO')
alter table INFORMES
   drop constraint FK_INFORMES_REFERENCE_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NOTICIAS') and o.name = 'FK_NOTICIAS_REFERENCE_USUARIO')
alter table NOTICIAS
   drop constraint FK_NOTICIAS_REFERENCE_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PLANTILLA') and o.name = 'FK_PLANTILL_REFERENCE_USUARIO')
alter table PLANTILLA
   drop constraint FK_PLANTILL_REFERENCE_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PLANTILLA') and o.name = 'FK_PLANTILL_REFERENCE_TIPO_PLA')
alter table PLANTILLA
   drop constraint FK_PLANTILL_REFERENCE_TIPO_PLA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SERVIDORESPUBLICOS') and o.name = 'FK_SERVIDORES_DEPTO')
alter table SERVIDORESPUBLICOS
   drop constraint FK_SERVIDORES_DEPTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BALAZO')
            and   type = 'U')
   drop table BALAZO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DEPARTAMENTOS')
            and   type = 'U')
   drop table DEPARTAMENTOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FOTOS')
            and   type = 'U')
   drop table FOTOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('INFORMACION')
            and   type = 'U')
   drop table INFORMACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('INFORMES')
            and   type = 'U')
   drop table INFORMES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NOTICIAS')
            and   type = 'U')
   drop table NOTICIAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PLANTILLA')
            and   type = 'U')
   drop table PLANTILLA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SERVIDORESPUBLICOS')
            and   type = 'U')
   drop table SERVIDORESPUBLICOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TIPO_INFORME')
            and   type = 'U')
   drop table TIPO_INFORME
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TIPO_PLANTILLA')
            and   type = 'U')
   drop table TIPO_PLANTILLA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO')
            and   type = 'U')
   drop table USUARIO
go

/*==============================================================*/
/* Table: BALAZO                                                */
/*==============================================================*/
create table BALAZO (
   IDBALAZO             int                  identity,
   IDNOTICIA            int                  null,
   DATOBALAZO           nvarchar(200)        not null,
   constraint PK_BALAZO primary key (IDBALAZO)
)
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
/* Table: FOTOS                                                 */
/*==============================================================*/
create table FOTOS (
   IDNOTICIA            int                  not null,
   IDFOTO               int                  identity,
   FOTOGRAFIA           varbinary(Max)       null,
   constraint PK_FOTOS primary key (IDNOTICIA)
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
/* Table: INFORMES                                              */
/*==============================================================*/
create table INFORMES (
   IDINFORME            int                  identity,
   IDTIPO               int                  null,
   IDUSUARIO            int                  null,
   NOMFREINFORME        nvarchar(50)         null,
   ARCHIVOINFORME       varbinary(Max)       null,
   constraint PK_INFORMES primary key (IDINFORME)
)
go

/*==============================================================*/
/* Table: NOTICIAS                                              */
/*==============================================================*/
create table NOTICIAS (
   IDNOTICIA            int                  identity,
   IDUSUARIO            int                  null,
   TITULO               nvarchar(225)        null,
   CONTENIDO            nvarchar(Max)        null,
   FECHAPUBLIC          datetime2            null,
   LUGAR                nvarchar(150)        null,
   PARTICIPANTES        nvarchar(300)        null,
   VER_COPLADENAY       bit                  null,
   VER_SEPLAN           bit                  null,
   VER_INTRANET         bit                  null,
   SOLO_MEDIOS          bit                  null,
   PRIORIDAD            tinyint              null,
   constraint PK_NOTICIAS primary key (IDNOTICIA)
)
go

/*==============================================================*/
/* Table: PLANTILLA                                             */
/*==============================================================*/
create table PLANTILLA (
   IDPLANTILLA          int                  not null,
   IDUSUARIO            int                  null,
   IDTIPO               int                  null,
   TITULO               nvarchar(100)        null,
   CONTENIDO            nvarchar(Max)        null,
   constraint PK_PLANTILLA primary key (IDPLANTILLA)
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
   EXTENSION            nvarchar(3)          null,
   FOTOPERSONAL         varbinary(Max)       null,
   CORREO               nvarchar(50)         null,
   CURRICULUM           varbinary(Max)       null,
   ESTATUS              nchar(1)             null,
   FECHAREGISTRO        Datetime             null,
   constraint PK_SERVIDORESPUBLICOS primary key (IDSERVPUB)
)
go

/*==============================================================*/
/* Table: TIPO_INFORME                                          */
/*==============================================================*/
create table TIPO_INFORME (
   IDTIPO               int                  identity,
   TIPOINFORME          nvarchar(50)         null,
   constraint PK_TIPO_INFORME primary key (IDTIPO)
)
go

/*==============================================================*/
/* Table: TIPO_PLANTILLA                                        */
/*==============================================================*/
create table TIPO_PLANTILLA (
   IDTIPO               int                  identity,
   TIPOPLANTILLA        nvarchar(50)         null,
   constraint PK_TIPO_PLANTILLA primary key (IDTIPO)
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
   FECHAREGISTRO        datetime2            null,
   constraint PK_USUARIO primary key (IDUSUARIO)
)
go

alter table BALAZO
   add constraint FK_BALAZO_REFERENCE_NOTICIAS foreign key (IDNOTICIA)
      references NOTICIAS (IDNOTICIA)
go

alter table FOTOS
   add constraint FK_FOTOS_REFERENCE_NOTICIAS foreign key (IDNOTICIA)
      references NOTICIAS (IDNOTICIA)
go

alter table INFORMES
   add constraint FK_INFORMES_REFERENCE_TIPO_INF foreign key (IDTIPO)
      references TIPO_INFORME (IDTIPO)
go

alter table INFORMES
   add constraint FK_INFORMES_REFERENCE_USUARIO foreign key (IDUSUARIO)
      references USUARIO (IDUSUARIO)
go

alter table NOTICIAS
   add constraint FK_NOTICIAS_REFERENCE_USUARIO foreign key (IDUSUARIO)
      references USUARIO (IDUSUARIO)
go

alter table PLANTILLA
   add constraint FK_PLANTILL_REFERENCE_USUARIO foreign key (IDUSUARIO)
      references USUARIO (IDUSUARIO)
go

alter table PLANTILLA
   add constraint FK_PLANTILL_REFERENCE_TIPO_PLA foreign key (IDTIPO)
      references TIPO_PLANTILLA (IDTIPO)
go

alter table SERVIDORESPUBLICOS
   add constraint FK_SERVIDORES_DEPTO foreign key (IDDEPARTAMENTO)
      references DEPARTAMENTOS (IDDEPARTAMENTO)
go


select * from USUARIO

	/*Tipos: 
	1 = Capturista
	2 = Administrador

	Estatus
	A= Activo
	I= Inactivo
		conexion:
	
	conexion: sqs.nayarit.gob.mx
usuario=usepladb 
PASSWORD='57wrTlp'

db name: sitio_sepla
	*/

INSERT INTO USUARIO
VALUES ('1234','1234', 'USUARIO CAPTURISTA',1,'A',GETDATE())

update USUARIO
set NOMBREUSUARIO = 'USUARIO ADMINISTRADOR'
where IDUSUARIO=1


drop table BALAZO
drop table DEPARTAMENTOS
drop table FOTOS
drop table INFORMACION
drop table INFORMES
drop table NOTICIAS
drop table PLANTILLA
drop table SERVIDORESPUBLICOS
drop table TIPO_INFORME
drop table TIPO_PLANTILLA*/