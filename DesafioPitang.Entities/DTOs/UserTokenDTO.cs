namespace DesafioPitang.Entities.DTOs
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserTokenDTO(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
