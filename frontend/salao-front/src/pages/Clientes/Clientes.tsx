import { useEffect, useState } from "react";
import {
  listarClientes,
  criarCliente,
  deletarCliente,
  atualizarCliente,
} from "../../services/clienteService";

import "./Clientes.css";
import MainLayout from "../../layouts/mainLayout/MainLayout";

export default function Clientes() {
  const [clientes, setClientes] = useState<any[]>([]);
  const [nome, setNome] = useState("");
  const [telefone, setTelefone] = useState("");
  const [editOpen, setEditOpen] = useState(false);
  const [clienteEdit, setClienteEdit] = useState<any>(null);

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const data = await listarClientes();
    setClientes(data);
  };

  const salvar = async () => {
    await criarCliente({ nome, telefone });

    setNome("");
    setTelefone("");

    load();
  };

  const remover = async (id: number) => {
    await deletarCliente(id);
    load();
  };

  const abrirEdicao = (cliente: any) => {
  setClienteEdit(cliente);
  setEditOpen(true);
  };

  const salvarEdicao = async () => {
    await atualizarCliente(clienteEdit.id, {
      nome: clienteEdit.nome,
      telefone: clienteEdit.telefone,
    });

    setEditOpen(false);
    setClienteEdit(null);

    load();
  };

  return (
    <MainLayout>
      <div className="container">
        <h1>Clientes</h1>

        {/* FORM */}
        <div style={{ marginBottom: 20 }}>
          <input
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />

          <input
            placeholder="Telefone"
            value={telefone}
            onChange={(e) => setTelefone(e.target.value)}
          />

          <button onClick={salvar}>Criar</button>
        </div>

        {/* LISTA */}
        <table border={1} cellPadding={10}>
          <thead>
            <tr>
              <th>Nome</th>
              <th>Telefone</th>
              <th>Ações</th>
            </tr>
          </thead>

          <tbody>
            {clientes.map((c) => (
              <tr key={c.id}>
                <td>{c.nome}</td>
                <td>{c.telefone}</td>
                <td>
                  <button onClick={() => abrirEdicao(c)}>Editar</button>
                  <button onClick={() => remover(c.id)}>Excluir</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        {editOpen && (
          <div className="modal-backdrop">
            <div className="modal">
              <h2>Editar Cliente</h2>

              <input
                value={clienteEdit?.nome || ""}
                onChange={(e) =>
                  setClienteEdit({ ...clienteEdit, nome: e.target.value })
                }
                placeholder="Nome"
              />

              <input
                value={clienteEdit?.telefone || ""}
                onChange={(e) =>
                  setClienteEdit({ ...clienteEdit, telefone: e.target.value })
                }
                placeholder="Telefone"
              />

              <div className="modal-actions">
                <button onClick={() => setEditOpen(false)}>Cancelar</button>
                <button onClick={salvarEdicao}>Salvar</button>
              </div>
            </div>
          </div>
        )}
      </div>
    </MainLayout>
  );
}