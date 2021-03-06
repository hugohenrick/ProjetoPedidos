ALTER PROCEDURE USUARIO_VALIDA_LOGIN (@EMAIL VARCHAR(100), 
                                        @SENHA VARCHAR(100), 
										@COD_USUARIO INT OUT, 
										@NOME VARCHAR(100) OUT, 
										@RETORNO VARCHAR(1000) OUT

) AS
BEGIN TRY

		SET @RETORNO = 'OK'
		SET @COD_USUARIO = 0
		SET @NOME = ''

		--VALIDAR LOGIN DO USUARIO--
		SELECT @COD_USUARIO = COD_USUARIO,
			   @NOME = NOME
		FROM   TAB_USUARIO
		WHERE  EMAIL = @EMAIL
		AND    SENHA = @SENHA

		IF ISNULL(@COD_USUARIO, 0) = 0
		BEGIN
			SET @RETORNO = 'E-mail ou senha inválida!'
			RETURN(0)
		END
END TRY

BEGIN CATCH
	SET @RETORNO = 'Ocorreu um erro ao validar usuário'
END CATCH

