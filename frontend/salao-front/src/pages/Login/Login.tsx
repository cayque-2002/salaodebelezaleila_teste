import { useState } from "react";
import { login } from "../../services/login";
import { setUser } from "../../services/userStorage";
import { useNavigate } from "react-router-dom";

import "./Login.css";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const user = await login(email, password);

      setUser(user);

      // 👇 redirecionamento por tipo de usuário
      if (user.tipo === 1) {
        navigate("/dashboard"); // Admin
        return;
      }

      navigate("/agendamentos"); // Cliente / comum
    } catch (error) {
      alert("Usuário ou senha inválidos");
    }
  };

  return (
    <div className="login-container">
      <div className="login-box">

        <h1>Salão Leila</h1>
        <p>Faça login para continuar</p>

        <input
          type="email"
          placeholder="E-mail"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Senha"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <button onClick={handleLogin}>
          Entrar
        </button>

      </div>
    </div>
  );
}