import React, { useState } from 'react';
import { Box, Flex, Link, VStack, Text, Button } from '@chakra-ui/react';
import { NavLink } from 'react-router-dom';

const Sidebar = () => {
  const [isOpen, setIsOpen] = useState(true); // Состояние для отслеживания видимости навбара

  return (
    <>
      <Box
        w="250px"
        bg="teal.500"
        color="white"
        position="fixed"
        h="100vh"
        p={5}
        transition="width 0.5s" // Добавляем анимацию для плавного изменения ширины
        sx={{
          visibility: isOpen ? 'visible' : 'hidden', // Используем состояние для управления видимостью
          width: isOpen ? '250px' : '0', // Используем состояние для управления шириной
        }}
      >
        <VStack spacing={6} align="flex-start">
          <Flex justify={"space-between"} >
          <Link to="#" mr={20} onClick={() => window.history.back()}>
          Go Back
          </Link>
          <Link to="#" onClick={() => window.history.forward()}>
          Go Ahead
          </Link>
          </Flex>
          
          <Text fontSize="2xl" fontWeight="bold">Sibers</Text>
          <Link as={NavLink} to="/" fontSize="lg" _hover={{ textDecoration: 'none', color: 'teal.200' }}>
            Home
          </Link>
          <Link as={NavLink} to="/employees" fontSize="lg" _hover={{ textDecoration: 'none', color: 'teal.200' }}>
            Employees
          </Link>
          <Link as={NavLink} to="/projects" fontSize="lg" _hover={{ textDecoration: 'none', color: 'teal.200' }}>
            Projects
          </Link>
          <Link as={NavLink} to="/tasks" fontSize="lg" _hover={{ textDecoration: 'none', color: 'teal.200' }}>
            Tasks
          </Link>
          <Link as={NavLink} to="/documents" fontSize="lg" _hover={{ textDecoration: 'none', color: 'teal.200' }}>
            Documents
          </Link>
        </VStack>
      </Box>
      {}
      <Button
        position="fixed"
        top="20px"
        left={isOpen ? '250px' : '0'}
        onClick={() => setIsOpen(!isOpen)}
      >
        {isOpen ? 'Скрыть' : 'Показать'}
      </Button>
    </>
  );
};

export default Sidebar;
