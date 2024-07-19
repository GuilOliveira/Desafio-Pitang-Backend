-- Criação da tabela de usuários
CREATE TABLE dbo.tb_usuario (
    id_usuario INT IDENTITY(1,1),
    dsc_email VARCHAR(255) NOT NULL UNIQUE,
    dsc_nome VARCHAR(255) NOT NULL,
    dsc_password_hash VARBINARY(MAX) NOT NULL,
    dsc_password_salt VARBINARY(MAX) NOT NULL,
    dsc_perfil VARCHAR(50) NOT NULL CHECK (dsc_perfil IN ('comum', 'admin')),
    dat_criacao DATETIME NOT NULL,
    CONSTRAINT PK_TB_USUARIO PRIMARY KEY (id_usuario)
);

-- Criação da tabela de pacientes
CREATE TABLE dbo.tb_paciente (
    id_paciente INT IDENTITY(1,1),
    dsc_nome VARCHAR(MAX) NOT NULL,
    dat_nascimento DATETIME NOT NULL,
    dat_criacao DATETIME NOT NULL,
    id_usuario INT NOT NULL,
    CONSTRAINT PK_TB_PACIENTE PRIMARY KEY (id_paciente),
    CONSTRAINT fk_paciente_usuario FOREIGN KEY (id_usuario)
        REFERENCES dbo.tb_usuario (id_usuario)
);

-- Criação da tabela de agendamentos
CREATE TABLE dbo.tb_agendamento (
    id_agendamento INT IDENTITY(1,1),
    id_paciente INT NOT NULL,
    dat_agendamento DATE NOT NULL,
    hor_agendamento TIME NOT NULL,
    dsc_status VARCHAR(50) NOT NULL,
    dat_criacao DATETIME NOT NULL,
    CONSTRAINT PK_TB_AGENDAMENTO PRIMARY KEY (id_agendamento),
    CONSTRAINT fk_agendamento_paciente FOREIGN KEY (id_paciente)
        REFERENCES dbo.tb_paciente (id_paciente)
);