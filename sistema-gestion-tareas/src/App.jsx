import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "./components/Login";
import Tasks from "./components/tasks/Tasks";
import Task from "./components/tasks/Task";
import Layout from "./components/Layout";
import { PrivateRoute } from "./PrivateRoute";

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/tasks" element={<PrivateRoute element={<Tasks />} />} />
          <Route path="/task/:id" element={<PrivateRoute element={<Task />} />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
