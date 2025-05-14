CREATE DATABASE love;
USE love;
CREATE TABLE pais (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

CREATE TABLE region (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50),
    pais_id INT,
    FOREIGN KEY (pais_id) REFERENCES pais(id)
);

CREATE TABLE ciudad (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50),
    region_id INT,
    FOREIGN KEY (region_id) REFERENCES region(id)
);

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50),
    edad INT,
    genero VARCHAR(50),
    carrera VARCHAR(50),
    frase_perfil TEXT,
    id_ciudad INT,
    FOREIGN KEY (id_ciudad) REFERENCES ciudad(id)
);

CREATE TABLE intereses_usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT,
    interes VARCHAR(100) NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);

CREATE TABLE estadisticas_usuario (
    id_usuario INT PRIMARY KEY,
    total_likes_recibidos INT DEFAULT 0,
    total_matches INT DEFAULT 0,
    total_likes_dados INT DEFAULT 0,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);

CREATE TABLE preferencias_usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT,
    preferencia_tipo VARCHAR(50) CHECK (preferencia_tipo IN ('edad', 'interes', 'carrera')),
    valor VARCHAR(100),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);

CREATE TABLE creditos_interaccion (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    fecha DATE NOT NULL,
    likes_usados INT DEFAULT 0,
    UNIQUE(id_usuario, fecha),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);


CREATE TABLE matches (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario_1 INT NOT NULL,
    id_usuario_2 INT NOT NULL,
    fecha_match TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (id_usuario_1, id_usuario_2),
    FOREIGN KEY (id_usuario_1) REFERENCES usuarios(id),
    FOREIGN KEY (id_usuario_2) REFERENCES usuarios(id)
);

CREATE TABLE interacciones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario_origen INT,
    id_usuario_destino INT,
    tipo_interaccion VARCHAR(10) CHECK (tipo_interaccion IN ('LIKE', 'DISLIKE')),
    fecha_interaccion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(id_usuario_origen, id_usuario_destino),
    FOREIGN KEY (id_usuario_origen) REFERENCES usuarios(id),
    FOREIGN KEY (id_usuario_destino) REFERENCES usuarios(id)
);

CREATE TABLE bloqueos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario_origen INT,
    id_usuario_bloqueado INT,
    fecha_bloqueo TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario_origen) REFERENCES usuarios(id),
    FOREIGN KEY (id_usuario_bloqueado) REFERENCES usuarios(id),
    UNIQUE(id_usuario_origen, id_usuario_bloqueado)
);
