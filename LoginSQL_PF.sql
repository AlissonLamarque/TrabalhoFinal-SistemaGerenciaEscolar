
drop user PF_GerenciaEscolar
create login PF_GerenciaEscolar with password='senha_PF'
create user PF_GerenciaEscolar from login PF_GerenciaEscolar

EXEC sp_addrolemember 'DB_DATAREADER', 'PF_GerenciaEscolar';
EXEC sp_addrolemember 'DB_DATAWRITER', 'PF_GerenciaEscolar';
