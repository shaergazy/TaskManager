// src/App.jsx
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { ChakraProvider, Flex, Box } from '@chakra-ui/react';
import Sidebar from './components/Navbar';
import Employees from './pages/Employees';
import Projects from './pages/projects';
import {Tasks} from './pages/Tasks';
import Home from './pages/Home';
import Documents from './pages/Documents';

function App() {
  return (
    <ChakraProvider>
      <Router>
        <Flex>
          <Sidebar />
          <Box ml="250px" p={5} flex="1">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/employees" element={<Employees />} />
              <Route path="/projects" element={<Projects />} />
              <Route path="/tasks" element={<Tasks/>} />
              <Route path="/projects/:projectId/tasks" element={<Tasks/>} />
              <Route path="/projects/:projectId/employees" element={<Employees/>} />
              <Route path="/documents" element={<Documents/>} />
            </Routes>
          </Box>
        </Flex>
      </Router>
    </ChakraProvider>
  );
}

export default App;
