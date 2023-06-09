USE [ObligatorioP32023]
GO
/****** Object:  Table [dbo].[Cabañas]    Script Date: 11/05/2023 19:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cabañas](
	[NumeroHabitacion] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](450) NOT NULL,
	[TipoId] [int] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[ConJacuzzi] [bit] NULL,
	[Habilitada] [bit] NULL,
	[CantPersonas] [int] NOT NULL,
	[Foto] [nvarchar](max) NULL,
 CONSTRAINT [PK_Cabañas] PRIMARY KEY CLUSTERED 
(
	[NumeroHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fotos]    Script Date: 11/05/2023 19:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fotos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Fotos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mantenimientos]    Script Date: 11/05/2023 19:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mantenimientos](
	[IdMantenimiento] [int] IDENTITY(1,1) NOT NULL,
	[IdCabaña] [int] NOT NULL,
	[FechaMantenimiento] [datetime2](7) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Costo] [int] NOT NULL,
	[Trabajador] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Mantenimientos] PRIMARY KEY CLUSTERED 
(
	[IdMantenimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametros]    Script Date: 11/05/2023 19:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Valor] [int] NOT NULL,
 CONSTRAINT [PK_Parametros] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipos]    Script Date: 11/05/2023 19:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](450) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[CostoPorHuesped] [int] NOT NULL,
 CONSTRAINT [PK_Tipos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/05/2023 19:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cabañas] ON 

INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1010, N'Cabaña Uno', 1008, N'Esta es la descripcion uno de la cabaña uno', 1, 0, 10, N'Cabaña Uno_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1011, N'Cabaña Dos', 1008, N'Esta es la descripcion de la Cabaña 2', 1, 1, 5, N'Cabaña Dos_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1012, N'Cabaña Tres', 1008, N'Esta es la descripcion de la Cabaña  3', 1, 1, 5, N'Cabaña Tres_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1013, N'Cabaña Premium', 1009, N'Esta es una de nuestras mejores Cabañas', 1, 0, 8, N'Cabaña Premium_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1014, N'Cabaña Aesthetic', 1008, N'Muy linda y acojedora', 1, 1, 6, N'Cabaña Aesthetic_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1015, N'Cabaña clasica', 1009, N'Cerca de muy buenas estaciones de servicios completas', 0, 1, 4, N'Cabaña clasica_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1016, N'Cabaña de en sueño', 1009, N'Perfecta para el invierno', 1, 1, 6, N'Cabaña de en sueño_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1017, N'Cabaña techada', 1008, N'Techada y mesa de ping pong', 1, 0, 7, N'Cabaña techada_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1018, N'Cabaña Capurcita Roja', 1008, N'En el corazon del bosque te espera esta increible cabaña', 0, 1, 9, N'Cabaña Capurcita Roja_1.jpg')
INSERT [dbo].[Cabañas] ([NumeroHabitacion], [Nombre], [TipoId], [Descripcion], [ConJacuzzi], [Habilitada], [CantPersonas], [Foto]) VALUES (1020, N'Ultima de nuesteras cabañas Premium', 1008, N'Ultra premium ideal para el frio', 1, 1, 5, N'Ultima de nuesteras cabañas Premium_1.jpg')
SET IDENTITY_INSERT [dbo].[Cabañas] OFF
GO
SET IDENTITY_INSERT [dbo].[Fotos] ON 

INSERT [dbo].[Fotos] ([Id]) VALUES (1)
SET IDENTITY_INSERT [dbo].[Fotos] OFF
GO
SET IDENTITY_INSERT [dbo].[Mantenimientos] ON 

INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1016, 1010, CAST(N'2023-05-11T18:04:00.0000000' AS DateTime2), N'Todo perfecto', 80, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1017, 1010, CAST(N'2023-05-11T18:11:00.0000000' AS DateTime2), N'Todo bien poco que hacer', 12, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1018, 1010, CAST(N'2023-05-10T18:12:00.0000000' AS DateTime2), N'Cortinas sucias un poco rajadas', 150, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1019, 1010, CAST(N'2023-05-09T18:12:00.0000000' AS DateTime2), N'Todo en optimas condiciones', 30, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1020, 1010, CAST(N'2023-05-08T18:12:00.0000000' AS DateTime2), N'Faltan alguans toallas', 30, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1021, 1010, CAST(N'2023-05-07T18:13:00.0000000' AS DateTime2), N'Todo en orden', 30, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1022, 1010, CAST(N'2023-05-06T18:14:00.0000000' AS DateTime2), N'Todo bien nada que comentar', 34, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1023, 1010, CAST(N'2023-05-05T18:14:00.0000000' AS DateTime2), N'Un pcoo sucio faltan desodorantes', 40, N'Gustavo')
INSERT [dbo].[Mantenimientos] ([IdMantenimiento], [IdCabaña], [FechaMantenimiento], [Descripcion], [Costo], [Trabajador]) VALUES (1024, 1010, CAST(N'2023-05-02T18:14:00.0000000' AS DateTime2), N'Limpiamos las hojas y alrededores', 55, N'Gustavo')
SET IDENTITY_INSERT [dbo].[Mantenimientos] OFF
GO
SET IDENTITY_INSERT [dbo].[Parametros] ON 

INSERT [dbo].[Parametros] ([Id], [Nombre], [Valor]) VALUES (1, N'TopeMinDescCabaña', 10)
INSERT [dbo].[Parametros] ([Id], [Nombre], [Valor]) VALUES (2, N'TopeMaxDescCabaña', 500)
INSERT [dbo].[Parametros] ([Id], [Nombre], [Valor]) VALUES (3, N'TopeMinDescTipo', 10)
INSERT [dbo].[Parametros] ([Id], [Nombre], [Valor]) VALUES (4, N'TopeMaxDescTipo', 200)
INSERT [dbo].[Parametros] ([Id], [Nombre], [Valor]) VALUES (5, N'TopeMinDescMantenimiento', 10)
INSERT [dbo].[Parametros] ([Id], [Nombre], [Valor]) VALUES (6, N'TopeMaxDescMantenimiento', 200)
SET IDENTITY_INSERT [dbo].[Parametros] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipos] ON 

INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1008, N'Aire Libre', N'Tipos de aire libre lejos de la ciudad', 50)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1009, N'Ciudad', N'Cerca de la ciudad, ruido moderado', 80)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1018, N'Cabaña de Montaña', N'Rústica y acogedora', 60)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1019, N'Apartamento en la Playa', N'Con vista al mar', 90)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1020, N'Hotel Boutique', N'Elegante y lujoso', 120)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1021, N'Casa Rural', N'Tranquilidad en el campo', 70)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1022, N'Hostal Urbano', N'Ambiente juvenil y económico', 40)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1023, N'Villa Exclusiva', N'Privacidad y comodidad', 150)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1024, N'Camping Familiar', N'Contacto directo con la naturaleza', 30)
INSERT [dbo].[Tipos] ([Id], [Nombre], [Descripcion], [CostoPorHuesped]) VALUES (1025, N'Cerca de Ciudad', N'Tipos de aire libre lejos de la ciudad', 50)
SET IDENTITY_INSERT [dbo].[Tipos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([Id], [Email], [Password]) VALUES (3, N'lucas@gmail.com', N'Lucas1')
INSERT [dbo].[Usuario] ([Id], [Email], [Password]) VALUES (4, N'mauro@gmail.com', N'Mauro1')
INSERT [dbo].[Usuario] ([Id], [Email], [Password]) VALUES (5, N'matias@gmail.com', N'Matias1')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Cabañas]  WITH CHECK ADD  CONSTRAINT [FK_Cabañas_Tipos_TipoId] FOREIGN KEY([TipoId])
REFERENCES [dbo].[Tipos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cabañas] CHECK CONSTRAINT [FK_Cabañas_Tipos_TipoId]
GO
ALTER TABLE [dbo].[Mantenimientos]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimientos_Cabañas_IdCabaña] FOREIGN KEY([IdCabaña])
REFERENCES [dbo].[Cabañas] ([NumeroHabitacion])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Mantenimientos] CHECK CONSTRAINT [FK_Mantenimientos_Cabañas_IdCabaña]
GO
