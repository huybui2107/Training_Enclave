import "./App.css";
import { Routes, Route } from "react-router-dom";
import Login from "./Page/Login";
import InformationUser from "./Page/InformationUser";

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/user" element={<InformationUser />} />
      </Routes>
    </>
  );
}

export default App;
