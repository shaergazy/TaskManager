// src/pages/Home.jsx
import React from 'react';
import { Box, Heading, Text, VStack, HStack, Icon } from '@chakra-ui/react';
import { FaTasks, FaProjectDiagram, FaUsers, FaCheckCircle, FaClock, FaChartLine } from 'react-icons/fa';

const Home = () => {
  return (
    <Box p={5} textAlign="center">
      <VStack spacing={6}>
        <Heading as="h1" size="2xl" color="teal.500">
          Добро пожаловать на платформу управления проектами
        </Heading>
        <Text fontSize="lg">
          Наша платформа предназначена для управления проектами, сотрудниками и задачами. Она предоставляет удобные инструменты для отслеживания прогресса, управления ресурсами и эффективного сотрудничества.
        </Text>

        <Heading as="h2" size="xl" color="teal.400">
          Основные функции платформы
        </Heading>
        <VStack spacing={4} align="start">
          <HStack spacing={3}>
            <Icon as={FaProjectDiagram} boxSize={6} color="teal.500" />
            <Text fontSize="md">Управление проектами: Создавайте, редактируйте и отслеживайте проекты легко и эффективно.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaUsers} boxSize={6} color="teal.500" />
            <Text fontSize="md">Управление сотрудниками: Управляйте информацией о сотрудниках, назначайте задачи и отслеживайте их прогресс.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaTasks} boxSize={6} color="teal.500" />
            <Text fontSize="md">Управление задачами: Создавайте, назначайте и контролируйте задачи для выполнения проектов в срок.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaCheckCircle} boxSize={6} color="teal.500" />
            <Text fontSize="md">Отслеживание выполнения: Мгновенный доступ к статусу выполнения задач и проектов.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaClock} boxSize={6} color="teal.500" />
            <Text fontSize="md">Управление временем: Планируйте и управляйте временными рамками проектов и задач.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaChartLine} boxSize={6} color="teal.500" />
            <Text fontSize="md">Аналитика и отчеты: Получайте подробные отчеты и аналитику для улучшения работы команды.</Text>
          </HStack>
        </VStack>

        <Heading as="h2" size="xl" color="teal.400">
          Преимущества использования нашей платформы
        </Heading>
        <VStack spacing={4} align="start">
          <HStack spacing={3}>
            <Icon as={FaUsers} boxSize={6} color="teal.500" />
            <Text fontSize="md">Повышение продуктивности: Оптимизируйте процессы и улучшите продуктивность команды.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaTasks} boxSize={6} color="teal.500" />
            <Text fontSize="md">Улучшенное сотрудничество: Легкое взаимодействие между членами команды для эффективного выполнения задач.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaCheckCircle} boxSize={6} color="teal.500" />
            <Text fontSize="md">Повышение качества: Постоянный контроль и улучшение качества выполнения проектов и задач.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaClock} boxSize={6} color="teal.500" />
            <Text fontSize="md">Экономия времени: Сокращение времени на управление проектами и задачами.</Text>
          </HStack>
          <HStack spacing={3}>
            <Icon as={FaChartLine} boxSize={6} color="teal.500" />
            <Text fontSize="md">Принятие обоснованных решений: Используйте данные и аналитику для принятия обоснованных решений.</Text>
          </HStack>
        </VStack>
      </VStack>
    </Box>
  );
};

export default Home;
